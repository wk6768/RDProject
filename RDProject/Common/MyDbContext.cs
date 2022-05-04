﻿using System;
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
            optionsBuilder.UseSqlServer("server=192.168.5.80;database=yanfa_test_1;uid=sa;pwd=123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(b => b.Id);
            modelBuilder.Entity<Employee>().HasIndex(b => b.Name);
            modelBuilder.Entity<Employee>().HasIndex(b => b.IsDeleted);
            

            modelBuilder.Entity<Trial>().HasKey(b => b.FHeadId);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FRDNo);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FProductName);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FCreateUser);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FCompany);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FTitle);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FStatus);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FDate);
            modelBuilder.Entity<Trial>().HasIndex(b => b.FCreateDate);


            modelBuilder.Entity<TrialEntry>().HasKey(b => b.FEntryId);
            modelBuilder.Entity<TrialEntry>().HasIndex(b => b.FHeadId);
            modelBuilder.Entity<TrialEntry>().HasIndex(b => b.FWorkOrder);
            modelBuilder.Entity<TrialEntry>().HasIndex(b => b.FProcessName);

            modelBuilder.Entity<WFInstance>().HasKey(b => b.InstanceId);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.TableName);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.InstanceGuid);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.Status);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.HeadId);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.SubBy);
            modelBuilder.Entity<WFInstance>().HasIndex(b => b.NextName);
            modelBuilder.Entity<WFInstance>().HasIndex(b => new { b.Status, b.NextName});

            modelBuilder.Entity<WFStep>().HasKey(b => b.StepId);
            modelBuilder.Entity<WFStep>().HasIndex(b => b.InstanceId);
            modelBuilder.Entity<WFStep>().HasIndex(b => b.BookMark);
            modelBuilder.Entity<WFStep>().HasIndex(b => b.SubBy);
            modelBuilder.Entity<WFStep>().HasIndex(b => b.Status);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        }
    }
}
