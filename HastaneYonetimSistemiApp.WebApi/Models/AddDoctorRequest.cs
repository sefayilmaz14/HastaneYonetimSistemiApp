using HastaneYonetimSistemiApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HastaneYonetimSistemiApp.WebApi.Models
{
    public class AddDoctorRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DoctorSpeciality DoctorSpeciality { get; set; }
        public string PhoneNumber { get; set; }
        public int PoliclinicId { get; set; }
    }
}
