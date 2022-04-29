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
            builder.Property(b => b.Status).HasDefaultValue(0);//0未审批，1已审批，2已驳回

            builder.Property(b => b.BookMark).HasMaxLength(32);
            builder.Property(b => b.Remark).HasMaxLength(128);
            builder.Property(b => b.SubBy).HasMaxLength(16);
        }
    }
}
