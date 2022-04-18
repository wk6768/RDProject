﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RDProject.Common;

namespace RDProject.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RDProject.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("UserGroup")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("RDProject.Models.Trial", b =>
                {
                    b.Property<long>("FHeadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FAssemblyFactory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FAssemblyNPI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FBillNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FCNCNPI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FCoatingNPI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FCompany")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 18, 15, 34, 3, 414, DateTimeKind.Local).AddTicks(2604));

                    b.Property<string>("FCreateUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FHasAssembly")
                        .HasColumnType("bit");

                    b.Property<bool>("FHasCNC")
                        .HasColumnType("bit");

                    b.Property<bool>("FHasCoating")
                        .HasColumnType("bit");

                    b.Property<bool>("FHasLaser")
                        .HasColumnType("bit");

                    b.Property<string>("FInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FLaserNPI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FRDNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FRequire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FWorkerOrderDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FHeadId");

                    b.ToTable("Trial");
                });

            modelBuilder.Entity("RDProject.Models.TrialEntry", b =>
                {
                    b.Property<long>("FEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("FBeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FCreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 18, 15, 34, 3, 415, DateTimeKind.Local).AddTicks(2660));

                    b.Property<DateTime>("FEndDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FHeadId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("FManHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FManPower")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FProcessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FWorkOrder")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FEntryId");

                    b.ToTable("TrialEntry");
                });

            modelBuilder.Entity("RDProject.Models.WFInstance", b =>
                {
                    b.Property<long>("InstanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("HeadId")
                        .HasColumnType("bigint");

                    b.Property<string>("InstanceGuid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("SubBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 18, 15, 34, 3, 405, DateTimeKind.Local).AddTicks(2598));

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstanceId");

                    b.ToTable("WFInstance");
                });

            modelBuilder.Entity("RDProject.Models.WFStep", b =>
                {
                    b.Property<long>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("InstanceId")
                        .HasColumnType("bigint");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("SubBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubTime")
                        .HasColumnType("datetime2");

                    b.HasKey("StepId");

                    b.ToTable("WFStep");
                });
#pragma warning restore 612, 618
        }
    }
}
