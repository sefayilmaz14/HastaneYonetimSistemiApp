using HastaneYonetimSistemiApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HastaneYonetimSistemiApp.WebApi.Models
{
    public class UpdatePatientRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
