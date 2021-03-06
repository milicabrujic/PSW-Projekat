// <auto-generated />
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

            modelBuilder.Entity("DrugPrescription", b =>
                {
                    b.Property<int>("DrugsId")
                        .HasColumnType("integer");

                    b.Property<int>("PrescriptionsId")
                        .HasColumnType("integer");

                    b.HasKey("DrugsId", "PrescriptionsId");

                    b.HasIndex("PrescriptionsId");

                    b.ToTable("DrugPrescription");
                });

            modelBuilder.Entity("PSW_backend.Models.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drug");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 15,
                            Name = "Brufen"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 20,
                            Name = "Aspirin"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 30,
                            Name = "Paracetamol"
                        });
                });

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
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientFeedback");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(2542),
                            IsPosted = true,
                            PatientId = 6,
                            Text = "Great Hospital."
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5525),
                            IsPosted = true,
                            PatientId = 7,
                            Text = "Great service."
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5606),
                            IsPosted = true,
                            PatientId = 6,
                            Text = "Great treetment."
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5615),
                            IsPosted = true,
                            PatientId = 8,
                            Text = "Nice doctors."
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5621),
                            IsPosted = true,
                            PatientId = 8,
                            Text = "Nice personel."
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5627),
                            IsPosted = true,
                            PatientId = 6,
                            Text = "Polite workers."
                        },
                        new
                        {
                            Id = 7,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5632),
                            IsPosted = false,
                            PatientId = 7,
                            Text = "Bad service."
                        },
                        new
                        {
                            Id = 8,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5637),
                            IsPosted = false,
                            PatientId = 7,
                            Text = "Impolite doctors."
                        },
                        new
                        {
                            Id = 9,
                            Date = new DateTime(2021, 7, 1, 23, 7, 21, 948, DateTimeKind.Local).AddTicks(5643),
                            IsPosted = false,
                            PatientId = 8,
                            Text = "Long waitings."
                        });
                });

            modelBuilder.Entity("PSW_backend.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescription");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DoctorId = 2,
                            PatientId = 6,
                            Text = "Take this medicine 3 times a week."
                        },
                        new
                        {
                            Id = 2,
                            DoctorId = 3,
                            PatientId = 7,
                            Text = "Take this medicine 2 times a day."
                        },
                        new
                        {
                            Id = 3,
                            DoctorId = 4,
                            PatientId = 6,
                            Text = "Take this medicine once a day."
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiQWRtaW5pc3RyYXRvciIsIklkIjoiMjMiLCJleHAiOjE2MjUwMDIwNzl9.04bjhzsBD-9bYL-8CtuYMYTnNvrvxrReah_eF8-Hbro",
                            Email = "admin@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "admin",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 2,
                            Surname = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("PSW_backend.Models.Doctor", b =>
                {
                    b.HasBaseType("PSW_backend.Models.User");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.ToTable("Doctor");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDAyODMyfQ.2eYr9v6qV-OnlBVAd23Y501ouDbhUbrKN_m5UnF2YJw",
                            Email = "ii@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Ivan",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 1,
                            Surname = "Ivanovic",
                            Username = "ii",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDc4MDgyfQ.FjZVW13Q2sQ4kpDqfPxS0LJDYDN1aVRc9B97FVZMAUA",
                            Email = "nn@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Nena",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 1,
                            Surname = "Nenic",
                            Username = "nn",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI0IiwiZXhwIjoxNjI1MDc4MTY3fQ.P-GUf_xNmiBTpzChik3CAuPx2FkWNGvtFaDMBNwI758",
                            Email = "dd@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Dara",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 1,
                            Surname = "Daric",
                            Username = "dd",
                            Type = 1
                        },
                        new
                        {
                            Id = 5,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI1IiwiZXhwIjoxNjI1MDc4NTEwfQ.9zWG0T0foHWCnuVGUofP-V-OEkPrAw9OfbJACog1m34",
                            Email = "vv@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Vera",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 1,
                            Surname = "Veric",
                            Username = "vv",
                            Type = 1
                        });
                });

            modelBuilder.Entity("PSW_backend.Models.Patient", b =>
                {
                    b.HasBaseType("PSW_backend.Models.User");

                    b.Property<int>("CancelledMedicalAppointments")
                        .HasColumnType("integer");

                    b.Property<int>("GeneralDoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastCanceledDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasIndex("GeneralDoctorId");

                    b.ToTable("Patient");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiMjEiLCJleHAiOjE2MjQ5OTY4OTd9.nsZgWxwWSeB-RxQFfvLSr5ruLoXtCFsHwzyABiYL7i0",
                            Email = "mb@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Milica",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 0,
                            Surname = "Brujic",
                            Username = "mb",
                            CancelledMedicalAppointments = 2,
                            GeneralDoctorId = 2,
                            LastCanceledDate = new DateTime(2021, 7, 1, 23, 7, 21, 944, DateTimeKind.Local).AddTicks(1612)
                        },
                        new
                        {
                            Id = 7,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiNyIsImV4cCI6MTYyNTA3ODY1NX0.2Fq9gQDhhfkVFg7_RoKTECIPZv9Aq0F1kmjWyHGB_Pw",
                            Email = "aa@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Ana",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 0,
                            Surname = "Antic",
                            Username = "aa",
                            CancelledMedicalAppointments = 1,
                            GeneralDoctorId = 2,
                            LastCanceledDate = new DateTime(2021, 7, 1, 23, 7, 21, 947, DateTimeKind.Local).AddTicks(6029)
                        },
                        new
                        {
                            Id = 8,
                            Address = "Laze Teleckog 1, Novi Sad",
                            AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiOCIsImV4cCI6MTYyNTA3ODg0N30.U2S_GgPYTsermyI_8jiZMvSqR--uPNE0M5Qo_mk6SRw",
                            Email = "ll@gmail.com",
                            IsBlocked = false,
                            IsMalicious = false,
                            Name = "Laza",
                            Password = "Admin123!",
                            PhoneNumber = "060000000",
                            Role = 0,
                            Surname = "Lazic",
                            Username = "ll",
                            CancelledMedicalAppointments = 1,
                            GeneralDoctorId = 3,
                            LastCanceledDate = new DateTime(2021, 7, 1, 23, 7, 21, 947, DateTimeKind.Local).AddTicks(6216)
                        });
                });

            modelBuilder.Entity("DrugPrescription", b =>
                {
                    b.HasOne("PSW_backend.Models.Drug", null)
                        .WithMany()
                        .HasForeignKey("DrugsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSW_backend.Models.Prescription", null)
                        .WithMany()
                        .HasForeignKey("PrescriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("PSW_backend.Models.Prescription", b =>
                {
                    b.HasOne("PSW_backend.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSW_backend.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

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
