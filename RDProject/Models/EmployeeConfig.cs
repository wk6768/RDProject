using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RDProject.Models
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        //public DbSet<Employee> Employees { get; set; }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));
            builder.Property(b => b.Id).HasMaxLength(16).IsRequired();
            builder.Property(b => b.Name).HasMaxLength(32).IsRequired();
            builder.Property(b => b.EmpName).HasMaxLength(32).IsRequired();
            builder.Property(b => b.Pwd).HasMaxLength(128).IsRequired();
            builder.Property(b => b.UserGroup).HasMaxLength(32);
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        }
    }
}
