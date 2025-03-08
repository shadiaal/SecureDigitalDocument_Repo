using Microsoft.AspNetCore.Mvc;
using SecureDocVerification.Models;
using SecureDocVerification.Services;
using System.Threading.Tasks;
using SecureDocVerification.Data;


namespace SecureDocVerification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        // Constructor injection to get the document service
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // POST /api/documents → Upload a document & generate a unique VerificationCode
        [HttpPost]

        public async Task<IActionResult> UploadDocument([FromBody] Document document)
        {
            // Check if the document is null
            if (document == null)
                return BadRequest("Invalid document data.");

            // Call the UploadDocument method in service layer to upload document and generate verification code
            var uploadedDocument = await _documentService.UploadDocumentAsync(document);

            // Return the uploaded document with a success response, including the ID of the newly created document
            return CreatedAtAction(nameof(GetDocumentById), new { id = uploadedDocument.Id }, uploadedDocument);

        }

        // GET: api/documents
        [HttpGet]
        public async Task<ActionResult<List<Document>>> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }


        // GET /api/documents/{id} → Retrieve document details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            // Retrieve the document using the ID from the service layer
            var document = await _documentService.GetDocumentByIdAsync(id);

            // If the document is not found, return a NotFound response
            if (document == null)
                return NotFound("Document not found.");

            // Return the document with a success response
            return Ok(document);
        }




        // POST /api/verify → Verify a document using Dapper instead of EF Core
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyDocument([FromBody] VerificationRequest request)
        {
            // Check if the request is valid
            if (request == null || string.IsNullOrEmpty(request.VerificationCode))
                return BadRequest("Invalid request data.");

            // Verify the document using Dapper
            var verificationResultDapper = await _documentService.VerifyDocumentAsync(request.VerificationCode, request.VerifiedBy);

            // If Dapper verification is successful, return success response
            if (verificationResultDapper)
                return Ok("Document verified successfully using Dapper.");

            // If Dapper verification fails, verify the document using EF Core
            var verificationResultEFCore = await _documentService.VerifyDocumentWithEFCoreAsync(request.VerificationCode, request.VerifiedBy);

            // If EF Core verification is successful, return success response
            if (verificationResultEFCore)
                return Ok("Document verified successfully using EF Core.");

            // If neither method is successful, return NotFound response
            return NotFound("Document not found or invalid verification code.");
        }
    }

    // For verification request
    public class VerificationRequest
    {
        public string VerificationCode { get; set; }
        public string VerifiedBy { get; set; }
    }
}


