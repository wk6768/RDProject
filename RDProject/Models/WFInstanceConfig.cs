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
        }
    }
}
