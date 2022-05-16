using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models
{
    public class SerialNumberConfig : IEntityTypeConfiguration<SerialNumber>
    {
        public void Configure(EntityTypeBuilder<SerialNumber> builder)
        {
            builder.ToTable(nameof(SerialNumber));
            builder.Property(b => b.FTableName).HasMaxLength(32);
        }
    }
}
