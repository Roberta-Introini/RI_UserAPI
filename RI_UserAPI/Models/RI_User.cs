using System.ComponentModel.DataAnnotations;

namespace RI_UserAPI.Models
{
    public class RI_User
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(128)]
        public string? FirstName { get; set; }

        [MaxLength(128)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        // validation is intentionally not put in the model 
        public int Age { get; set; }
    }
}

