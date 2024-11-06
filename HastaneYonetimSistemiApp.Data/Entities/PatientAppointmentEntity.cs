using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class PatientAppointmentEntity : BaseEntity
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        //Relational Property

        public PatientEntity Patient { get; set; }
        public AppointmentsEntity Appointment { get; set; }
    }

    public class PatientAppointmentConfiguration : BaseConfiguration<PatientAppointmentEntity>
    {
        public override void Configure(EntityTypeBuilder<PatientAppointmentEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("PatientId", "AppointmentId");

            // PatientAppointmentEntity ile AppointmentsEntity arasındaki ilişki tanımlaması
            builder.HasOne(pa => pa.Appointment)          // PatientAppointmentEntity'nin Appointment ilişkisi
                   .WithMany(a => a.PatientAppointments)  // AppointmentsEntity içinde PatientAppointments koleksiyonu
                   .HasForeignKey(pa => pa.AppointmentId); // Foreign key tanımlaması

            base.Configure(builder);
        }
    }

}
