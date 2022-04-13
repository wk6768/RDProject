using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RDProject.Models
{
    public class WFStepConfig : IEntityTypeConfiguration<WFStep>
    {
        public void Configure(EntityTypeBuilder<WFStep> builder)
        {
            builder.ToTable(nameof(WFStep));
            builder.Property(b => b.Status).HasDefaultValue(1);
        }
    }
}
