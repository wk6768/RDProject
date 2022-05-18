using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models
{
    internal class ManpowerEntryConfig : IEntityTypeConfiguration<ManpowerEntry>
    {
        public void Configure(EntityTypeBuilder<ManpowerEntry> builder)
        {
            builder.ToTable(nameof(ManpowerEntry));

            builder.Property(b => b.FCreateDate).HasDefaultValue(DateTime.Now);
            builder.Property(b => b.FEmpName).HasMaxLength(16);
            builder.Property(b => b.FDeptName).HasMaxLength(16);
        }
    }
}
