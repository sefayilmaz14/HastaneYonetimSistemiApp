using HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Doctor
{
    public interface IDoctorService
    {
        Task<DoctorDto> GetDoctor(int id);
        Task<List<DoctorDto>> GetAllDoctors();
        Task<ServiceMassage> AddDoctor(AddDoctorDto doctor);
        Task<ServiceMassage> EditDoctorFirstName(int id, string changeTo);
        Task<ServiceMassage> EditDoctorLastName(int id, string changeTo);
        Task<ServiceMassage> EditDoctorSpeciality(int id, DoctorSpeciality changeTo);
        Task<ServiceMassage> EditPhoneNumber(int id, string changeTo);
        Task<ServiceMassage> EditPoliclinicId(int id, int changeTo);
        Task<ServiceMassage> DeleteDoctor(int id);
        Task<ServiceMassage> UpdateDoctor(UpdateDoctorDto doctor);

    }
}
