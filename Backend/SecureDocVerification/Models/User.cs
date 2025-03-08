using System.ComponentModel.DataAnnotations;

namespace SecureDocVerification.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // Admin, User

        // One-to-many relationship with Documents
        public List<Document>? Documents { get; set; }
    }
}
