using HastaneYonetimSistemiApp.Data.Enums;

namespace HastaneYonetimSistemiApp.WebApi.Models
{
    public class AddAppointmentRequest
    {
        
        public List<int> PatientId { get; set; }
        public List<int> DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public int RoomNumber { get; set; }
        public string AppointmentType { get; set; }
    }
}
