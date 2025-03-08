using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureDocVerification.Models
{
    public class VerificationLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public Document? Document { get; set; }

        [Required]
        public string VerifiedBy { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status { get; set; } // Success, Failed
    }
}
