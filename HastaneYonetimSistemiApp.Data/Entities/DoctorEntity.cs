using HastaneYonetimSistemiApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class DoctorEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpeciality DoctorSpeciality { get; set; }
        public string PhoneNumber { get; set; }
        public int PoliclinicId { get; set; }

        //Relational Property

        public ICollection<DoctorAppointmentEntity> DoctorAppointments { get; set; }

    }

    public class DoctorConfiguration : BaseConfiguration<DoctorEntity> 
    {

        public override void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {

            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
