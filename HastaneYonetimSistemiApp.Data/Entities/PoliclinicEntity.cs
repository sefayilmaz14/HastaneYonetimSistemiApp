using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneYonetimSistemiApp.Data.Entities
{
    public class PoliclinicEntity :BaseEntity
    {
        public string Title { get; set; }
        public int PoliclinicNumber { get; set; }
    }

    public class PoliclinicConfiguration : BaseConfiguration<PoliclinicEntity> 
    {
        public override void Configure(EntityTypeBuilder<PoliclinicEntity> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);
            
            base.Configure(builder);
        }
    }
}
