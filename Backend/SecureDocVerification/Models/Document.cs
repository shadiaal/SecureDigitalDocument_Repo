using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureDocVerification.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public string Status { get; set; } // Pending, Verified, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // One-to-many relationship with VerificationLogs
        public List<VerificationLog>? VerificationLogs { get; set; }
    }
}
