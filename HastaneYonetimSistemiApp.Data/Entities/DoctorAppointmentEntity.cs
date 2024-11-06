using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class DoctorAppointmentEntity : BaseEntity
    {
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }

        //Relational Property

        public DoctorEntity Doctor { get; set; }
        public AppointmentsEntity Appointment { get; set; }
    }

    public class DoctorAppointmentConfiguration : BaseConfiguration<DoctorAppointmentEntity>
    {
        public override void Configure(EntityTypeBuilder<DoctorAppointmentEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("DoctorId", "AppointmentId");

            // DoctorAppointmentEntity ile AppointmentsEntity arasındaki ilişki tanımlaması
            builder.HasOne(da => da.Appointment)           // DoctorAppointmentEntity'nin Appointment ilişkisi
                   .WithMany(a => a.DoctorAppointments)    // AppointmentsEntity içinde DoctorAppointments koleksiyonu
                   .HasForeignKey(da => da.AppointmentId); // Foreign key tanımlaması

            base.Configure(builder);
        }
    }

}
