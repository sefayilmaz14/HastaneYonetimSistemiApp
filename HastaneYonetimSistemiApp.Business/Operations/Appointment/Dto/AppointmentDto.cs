using HastaneYonetimSistemiApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Business.Operations.Appointment.Dto
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public int RoomNumber { get; set; }
        public string AppointmentType { get; set; }
    }
}
