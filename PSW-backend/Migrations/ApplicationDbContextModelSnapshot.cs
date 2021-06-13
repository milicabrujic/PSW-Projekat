﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PSW_backend.Models;

namespace PSW_backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("PSW_backend.Models.MedicalAppointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalAppointment");
                });

            modelBuilder.Entity("PSW_backend.Models.PatientFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsPosted")
                        .HasColumnType("boolean");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientFeedback");
                });

            modelBuilder.Entity("PSW_backend.Models.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialistDoctorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("SpecialistDoctorId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("PSW_backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("AuthenticationToken")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMalicious")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Password")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PSW_backend.Models.Administrator", b =>
                {
                    b.HasBaseType("PSW_backend.Models.User");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("PSW_backend.Models.Doctor", b =>
                {
                    b.HasBaseType("PSW_backend.Models.User");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("PSW_backend.Models.Patient", b =>
                {
                    b.HasBaseType("PSW_backend.Models.User");

                    b.Property<int>("CancelledMedicalAppointments")
                        .HasColumnType("integer");

                    b.Property<int>("GeneralDoctorId")
                        .HasColumnType("integer");

                    b.HasIndex("GeneralDoctorId");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("PSW_backend.Models.MedicalAppointment", b =>
                {
                    b.HasOne("PSW_backend.Models.Doctor", "Doctor")
                        .WithMany("MedicalAppointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSW_backend.Models.Patient", "Patient")
                        .WithMany("MedicalAppointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PSW_backend.Models.PatientFeedback", b =>
                {
                    b.HasOne("PSW_backend.Models.Patient", "Patient")
                        .WithMany("PatientFeedbacks")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PSW_backend.Models.Recommendation", b =>
                {
                    b.HasOne("PSW_backend.Models.Patient", "Patient")
                        .WithMany("Recommendations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSW_backend.Models.Doctor", "SpecialistDoctor")
                        .WithMany("Recommendations")
                        .HasForeignKey("SpecialistDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("SpecialistDoctor");
                });

            modelBuilder.Entity("PSW_backend.Models.Administrator", b =>
                {
                    b.HasOne("PSW_backend.Models.User", null)
                        .WithOne()
                        .HasForeignKey("PSW_backend.Models.Administrator", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSW_backend.Models.Doctor", b =>
                {
                    b.HasOne("PSW_backend.Models.User", null)
                        .WithOne()
                        .HasForeignKey("PSW_backend.Models.Doctor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSW_backend.Models.Patient", b =>
                {
                    b.HasOne("PSW_backend.Models.Doctor", "GeneralDoctor")
                        .WithMany("Patients")
                        .HasForeignKey("GeneralDoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSW_backend.Models.User", null)
                        .WithOne()
                        .HasForeignKey("PSW_backend.Models.Patient", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GeneralDoctor");
                });

            modelBuilder.Entity("PSW_backend.Models.Doctor", b =>
                {
                    b.Navigation("MedicalAppointments");

                    b.Navigation("Patients");

                    b.Navigation("Recommendations");
                });

            modelBuilder.Entity("PSW_backend.Models.Patient", b =>
                {
                    b.Navigation("MedicalAppointments");

                    b.Navigation("PatientFeedbacks");

                    b.Navigation("Recommendations");
                });
#pragma warning restore 612, 618
        }
    }
}
