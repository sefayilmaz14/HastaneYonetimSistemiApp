using HastaneYonetimSistemiApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class AppointmentsEntity : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public int RoomNumber { get; set; }
        public string AppointmentType { get; set; }

        //Relational Property

        public ICollection<DoctorAppointmentEntity> DoctorAppointments { get; set; }
        public ICollection<PatientAppointmentEntity> PatientAppointments { get; set; }
    }

    public class AppointmentConfiguration : BaseConfiguration<AppointmentsEntity>
    {
        public override void Configure(EntityTypeBuilder<AppointmentsEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
