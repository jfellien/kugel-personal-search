﻿// <auto-generated />
using System;
using Kugel.StaffSearch.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kugel.StaffSearch.Database.SqlServer.Migrations.Migrations
{
    [DbContext(typeof(KugelStaffSearchContext))]
    partial class KugelPersonalSearchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kugel.StaffSearch.Database.Entities.StaffMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaffMember");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e9774bcb-191a-48cc-8295-23dfa452bfad"),
                            FirstName = "Petra",
                            LastName = "Bauknecht",
                            PersonalId = "2023-01-01-01"
                        },
                        new
                        {
                            Id = new Guid("c6fea08e-620d-4a64-915a-77cad7d4b140"),
                            FirstName = "Philipp",
                            LastName = "Bauknecht",
                            PersonalId = "2023-01-01-02"
                        },
                        new
                        {
                            Id = new Guid("56df7680-047e-4796-9f2e-00e3094b212a"),
                            FirstName = "Janek",
                            LastName = "Fellien",
                            PersonalId = "2023-01-01-03"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
