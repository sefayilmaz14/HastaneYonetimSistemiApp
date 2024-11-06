using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto
{
    public class AddDoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpeciality DoctorSpeciality { get; set; }
        public string PhoneNumber { get; set; }
        public int PoliclinicId { get; set; }
    }
}
