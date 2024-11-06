using HastaneYonetimSistemiApp.Business.Operations.Doctor.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Patient
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatient(int id);
        Task<List<PatientDto>> GetAllPatients();
        Task<ServiceMassage> AddPatient(AddPatientDto patient);
        Task<ServiceMassage> EditPatientFirstName(int id, string changeTo);
        Task<ServiceMassage> EditPatientLastName(int id, string changeTo);
        Task<ServiceMassage> EditBirthDate(int id, DateTime changeTo);
        Task<ServiceMassage> EditGender(int id, Gender changeTo);
        Task<ServiceMassage> DeletePatient(int id);
        Task<ServiceMassage> UpdatePatient(UpdatePatientDto patient);
    }
}
