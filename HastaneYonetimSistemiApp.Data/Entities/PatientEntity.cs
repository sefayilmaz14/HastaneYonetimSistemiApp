using HastaneYonetimSistemiApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class PatientEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        //Relational Property

        public ICollection<PatientAppointmentEntity> PatientAppointments { get; set; }
    }

    public class PatientConfiguration : BaseConfiguration <PatientEntity> 
    {
        public override void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            
            base.Configure(builder);
        }
    }   
}
