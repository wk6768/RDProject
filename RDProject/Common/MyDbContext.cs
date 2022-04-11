using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDProject.Models;

namespace RDProject.Common
{
    public class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Trial> Trials { get; set; }
        public DbSet<TrialEntry> TrialEntries { get; set; }
        public DbSet<WFInstance> WFInstances { get; set; }
        public DbSet<WFStep> WFSteps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=127.0.0.1;database=yanfa_test_1;uid=sa;pwd=123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(b => b.IsDeleted);
            modelBuilder.Entity<Trial>().HasKey(b => b.FHeadId);
            modelBuilder.Entity<TrialEntry>().HasKey(b => b.FEntryId);
            modelBuilder.Entity<WFInstance>().HasKey(b => b.HeadId);
            modelBuilder.Entity<WFStep>().HasKey(b => b.StepId);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        }
    }
}
