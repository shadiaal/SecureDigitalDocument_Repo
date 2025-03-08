using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SecureDocVerification.Models;
using System.Data;
using System.Diagnostics;  // Import Stopwatch for performance tracking
using System.Threading.Tasks;
using SecureDocVerification.Data;
using Microsoft.EntityFrameworkCore;

namespace SecureDocVerification.Services
{
    public interface IDocumentService
    {
        Task<Document> UploadDocumentAsync(Document document);
        Task<Document> GetDocumentByIdAsync(int id);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<bool> VerifyDocumentAsync(string verificationCode, string verifiedBy);
        Task<bool> VerifyDocumentWithEFCoreAsync(string verificationCode, string verifiedBy); // Added EF comparison method
    }

    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        // Constructor to inject the database connection string and AppDbContext
        public DocumentService(IConfiguration configuration, AppDbContext context)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _context = context;
        }

        // Upload a document & generate a unique VerificationCode
        public async Task<Document> UploadDocumentAsync(Document document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Generate a unique verification code
                document.VerificationCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
                document.Status = "Pending";  // Initial status is Pending
                document.CreatedAt = DateTime.UtcNow;

                var query = "INSERT INTO Documents (UserId, Title, FilePath, VerificationCode, Status, CreatedAt) " +
                            "VALUES (@UserId, @Title, @FilePath, @VerificationCode, @Status, @CreatedAt); " +
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);"; // Get the new document ID

                // Execute the query and return the document with the new ID
                var documentId = await connection.QuerySingleAsync<int>(query, document);
                document.Id = documentId;
                return document;
            }
        }
       
        public async Task<List<Document>> GetAllDocumentsAsync()
        {
           
            return await _context.Documents.ToListAsync();
        }
    
    // Get document by id
    public async Task<Document> GetDocumentByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Documents WHERE Id = @Id";
                // Retrieve the document by its ID
                return await connection.QueryFirstOrDefaultAsync<Document>(query, new { Id = id });
            }
        }

        // Verify document using Dapper (performance optimization)
        public async Task<bool> VerifyDocumentAsync(string verificationCode, string verifiedBy)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Start measuring time for performance analysis using Stopwatch
                var stopwatch = Stopwatch.StartNew();

                // Query to verify the document using the verification code
                var query = "SELECT * FROM Documents WHERE VerificationCode = @Code AND Status = 'Pending'";
                var document = await connection.QueryFirstOrDefaultAsync<Document>(query, new { Code = verificationCode });

                // If the document is not found, stop and log the time
                if (document == null)
                {
                    stopwatch.Stop();
                    Console.WriteLine($"[Dapper] Verification failed in {stopwatch.ElapsedMilliseconds} ms.");
                    return false;
                }

                // Update the document status to 'Verified'
                var updateQuery = "UPDATE Documents SET Status = 'Verified' WHERE Id = @DocumentId";
                await connection.ExecuteAsync(updateQuery, new { DocumentId = document.Id });

                // Log the verification process
                var logQuery = "INSERT INTO VerificationLogs (DocumentId, VerifiedBy, Status, Timestamp) " +
                               "VALUES (@DocumentId, @VerifiedBy, 'Success', @Timestamp)";

                await connection.ExecuteAsync(logQuery, new
                {
                    DocumentId = document.Id,
                    VerifiedBy = verifiedBy,
                    Timestamp = DateTime.UtcNow
                });

                stopwatch.Stop();
                // Log the time taken for the verification process using Dapper
                Console.WriteLine($"[Dapper] Verification completed in {stopwatch.ElapsedMilliseconds} ms.");

                return true;
            }
        }

        // Example: Verify document using Entity Framework Core (for performance comparison)
        public async Task<bool> VerifyDocumentWithEFCoreAsync(string verificationCode, string verifiedBy)
        {
            var stopwatch = Stopwatch.StartNew(); // Start measuring time

            // Use the injected AppDbContext
            var document = await _context.Documents
                .FirstOrDefaultAsync(d => d.VerificationCode == verificationCode && d.Status == "Pending");

            if (document == null)
            {
                stopwatch.Stop();
                Console.WriteLine($"[EF Core] Verification failed in {stopwatch.ElapsedMilliseconds} ms.");
                return false;
            }

            // Update the document status to 'Verified' using EF Core
            document.Status = "Verified";
            await _context.SaveChangesAsync();

            // Log the verification process using EF Core
            var log = new VerificationLog
            {
                DocumentId = document.Id,
                VerifiedBy = verifiedBy,
                Status = "Success",
                Timestamp = DateTime.UtcNow
            };
            _context.VerificationLogs.Add(log);
            await _context.SaveChangesAsync();

            stopwatch.Stop();
            // Log the time taken for the verification process using EF Core
            Console.WriteLine($"[EF Core] Verification completed in {stopwatch.ElapsedMilliseconds} ms.");

            return true;
        }
    }
}
