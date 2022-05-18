using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models
{
    public class ManpowerConfig : IEntityTypeConfiguration<Manpower>
    {
        public void Configure(EntityTypeBuilder<Manpower> builder)
        {
            builder.ToTable(nameof(Manpower));

            builder.Property(b => b.FCreateUser).HasMaxLength(16);
            builder.Property(b => b.FCreateDate).HasDefaultValue(DateTime.Now);
            builder.Property(b => b.FTitle).HasMaxLength(64);
            builder.Property(b => b.FCompany).HasMaxLength(32);
        }
    }
}
