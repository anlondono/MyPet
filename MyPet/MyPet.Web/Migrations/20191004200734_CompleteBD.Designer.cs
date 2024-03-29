﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPet.Web.Data;

namespace MyPet.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191004200734_CompleteBD")]
    partial class CompleteBD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyPet.Web.Data.Entities.Adopter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Adopters");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("Description");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("PetImageId");

                    b.Property<int?>("PetTypeId");

                    b.Property<int?>("RaceId");

                    b.Property<int?>("TemporaryOwnerId");

                    b.HasKey("Id");

                    b.HasIndex("PetImageId");

                    b.HasIndex("PetTypeId");

                    b.HasIndex("RaceId");

                    b.HasIndex("TemporaryOwnerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.PetImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("PetImageId");

                    b.HasKey("Id");

                    b.HasIndex("PetImageId");

                    b.ToTable("PetImages");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.PetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("PetTypes");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdopterId");

                    b.Property<int?>("PetId");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.TemporaryOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FixedPhone")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TemporaryOwners");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.Pet", b =>
                {
                    b.HasOne("MyPet.Web.Data.Entities.PetImage", "PetImage")
                        .WithMany()
                        .HasForeignKey("PetImageId");

                    b.HasOne("MyPet.Web.Data.Entities.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId");

                    b.HasOne("MyPet.Web.Data.Entities.Race", "Race")
                        .WithMany("Pets")
                        .HasForeignKey("RaceId");

                    b.HasOne("MyPet.Web.Data.Entities.TemporaryOwner", "TemporaryOwner")
                        .WithMany("Pets")
                        .HasForeignKey("TemporaryOwnerId");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.PetImage", b =>
                {
                    b.HasOne("MyPet.Web.Data.Entities.PetImage")
                        .WithMany("PetImages")
                        .HasForeignKey("PetImageId");
                });

            modelBuilder.Entity("MyPet.Web.Data.Entities.Request", b =>
                {
                    b.HasOne("MyPet.Web.Data.Entities.Adopter")
                        .WithMany("Requests")
                        .HasForeignKey("AdopterId");

                    b.HasOne("MyPet.Web.Data.Entities.Pet", "Pet")
                        .WithMany("Requests")
                        .HasForeignKey("PetId");
                });
#pragma warning restore 612, 618
        }
    }
}
