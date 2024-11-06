using System.ComponentModel.DataAnnotations;

namespace HastaneYonetimSistemiApp.WebApi.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
