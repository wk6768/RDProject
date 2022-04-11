using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RDProject.Models
{
    public class TrialEntryConfig : IEntityTypeConfiguration<TrialEntry>
    {
        public void Configure(EntityTypeBuilder<TrialEntry> builder)
        {
            builder.ToTable(nameof(TrialEntry));
        }
    }
}
