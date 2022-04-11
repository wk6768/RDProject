using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RDProject.Models
{
    public class TrialConfig : IEntityTypeConfiguration<Trial>
    {
        public void Configure(EntityTypeBuilder<Trial> builder)
        {
            builder.ToTable(nameof(Trial));
        }
    }
}
