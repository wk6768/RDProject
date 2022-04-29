using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RDProject.Models
{
    public class WFInstanceConfig : IEntityTypeConfiguration<WFInstance>
    {
        public void Configure(EntityTypeBuilder<WFInstance> builder)
        {
            builder.ToTable(nameof(WFInstance));
            builder.Property(b => b.SubTime).HasDefaultValue(DateTime.Now);
            builder.Property(b => b.Status).HasDefaultValue(0);

            builder.Property(b => b.TableName).HasMaxLength(32);
            builder.Property(b => b.InstanceGuid).HasMaxLength(64);
            builder.Property(b => b.SubBy).HasMaxLength(16);
            builder.Property(b => b.NextName).HasMaxLength(16);
        }
    }
}
