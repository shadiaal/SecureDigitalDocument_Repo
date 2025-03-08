using Microsoft.EntityFrameworkCore;
using SecureDocVerification.Models;
using System;

namespace SecureDocVerification.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defining the relationship between User and Document (one-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Documents)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

            // Defining the relationship between Document and VerificationLog (one-to-many)
            modelBuilder.Entity<Document>()
                .HasMany(d => d.VerificationLogs)
                .WithOne(v => v.Document)
                .HasForeignKey(v => v.DocumentId);

            // Seeding default Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin User", Email = "admin@example.com", Password = "admin123", Role = "Admin" },
                new User { Id = 2, Name = "Regular User", Email = "user@example.com", Password = "user123", Role = "User" }
            );

            // Seeding default Documents
            modelBuilder.Entity<Document>().HasData(
                new Document { Id = 1, UserId = 1, Title = "Sample Document 1", FilePath = "/docs/sample1.pdf", VerificationCode = "ABC123", Status = "Pending", CreatedAt = DateTime.UtcNow },
                new Document { Id = 2, UserId = 2, Title = "Sample Document 2", FilePath = "/docs/sample2.pdf", VerificationCode = "XYZ456", Status = "Pending", CreatedAt = DateTime.UtcNow }
            );

            // Seeding default VerificationLogs
            modelBuilder.Entity<VerificationLog>().HasData(
                new VerificationLog { Id = 1, DocumentId = 1, VerifiedBy = "admin@example.com", Timestamp = DateTime.UtcNow, Status = "Success" },
                new VerificationLog { Id = 2, DocumentId = 2, VerifiedBy = "user@example.com", Timestamp = DateTime.UtcNow, Status = "Failed" }
            );
        }
    }
}
