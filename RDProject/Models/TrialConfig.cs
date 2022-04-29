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
            builder.Property(b => b.FCreateDate).HasDefaultValue(DateTime.Now);
            builder.Property(b => b.FStatus).HasDefaultValue(0);

            builder.Property(b => b.FCreateUser).HasMaxLength(16);
            builder.Property(b => b.FTitle).HasMaxLength(64);
            builder.Property(b => b.FBillNo).HasMaxLength(32);
            builder.Property(b => b.FRDNo).HasMaxLength(32);
            builder.Property(b => b.FProductName).HasMaxLength(64);
            builder.Property(b => b.FWorkerOrderDescription).HasMaxLength(128);
            builder.Property(b => b.FCompany).HasMaxLength(32);
            builder.Property(b => b.FCNCNPI).HasMaxLength(16);
            builder.Property(b => b.FCoatingNPI).HasMaxLength(16);
            builder.Property(b => b.FLaserNPI).HasMaxLength(16);
            builder.Property(b => b.FAssemblyNPI).HasMaxLength(16);
            builder.Property(b => b.FAssemblyFactory).HasMaxLength(16);
            builder.Property(b => b.FInformation).HasMaxLength(128);
            builder.Property(b => b.FRequire).HasMaxLength(128);
        }
    }
}
