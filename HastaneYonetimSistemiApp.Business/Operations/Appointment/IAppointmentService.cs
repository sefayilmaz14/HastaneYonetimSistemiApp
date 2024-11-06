using HastaneYonetimSistemiApp.Business.Operations.Appointment.Dto;
using HastaneYonetimSistemiApp.Business.Operations.Patient.Dto;
using HastaneYonetimSistemiApp.Business.Types;
using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Appointment
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointment(int id);
        Task<List<AppointmentDto>> GetAllAppointments();
        Task<List<DoctorAppointmentDto>> GetDoctorAppointment(int id);
        Task<List<PatientAppointmentDto>> GetPatientAppointment(int id);
        Task<ServiceMassage> AddAppointment(AddAppointmentDto appointment);
        Task<ServiceMassage> EditAppointmentDoctorId(int id, int changeTo);
        Task<ServiceMassage> EditAppointmentPatientId(int id, int changeTo);
        Task<ServiceMassage> EditAppointmentDate(int id, DateTime changeTo);
        Task<ServiceMassage> EditAppointmentStatus(int id, AppointmentStatus changeTo);
        Task<ServiceMassage> EditAppointmentRoomNumber(int id, int changeTo);
        Task<ServiceMassage> EditAppointmentType(int id, string changeTo);
        Task<ServiceMassage> DeleteAppointment(int id);
        
    }
}
