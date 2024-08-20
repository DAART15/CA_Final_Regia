﻿// <auto-generated />
using System;
using CA_Final_Regia.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CA_Final_Regia.Infrastructure.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20240820093415_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Location", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApartmentNr")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNr")
                        .HasColumnType("int");

                    b.Property<long>("PersonalId")
                        .HasColumnType("bigint");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PersonalId")
                        .HasMaxLength(11)
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("AccountId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Location", b =>
                {
                    b.HasOne("CA_Final_Regia.Domain.Models.Person", "Person")
                        .WithOne("Location")
                        .HasForeignKey("CA_Final_Regia.Domain.Models.Location", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Person", b =>
                {
                    b.HasOne("CA_Final_Regia.Domain.Models.Account", "Account")
                        .WithOne("Person")
                        .HasForeignKey("CA_Final_Regia.Domain.Models.Person", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Account", b =>
                {
                    b.Navigation("Person");
                });

            modelBuilder.Entity("CA_Final_Regia.Domain.Models.Person", b =>
                {
                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
