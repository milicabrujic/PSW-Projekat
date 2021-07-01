using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<MedicalAppointment> MedicalAppointments { get; set; }
        public DbSet<PatientFeedback> PatientFeedback { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Prescription> Prescriptions  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //admin
            builder.Entity<Administrator>().HasData(
               new Administrator
               {
                   Id = 1,
                   Name = "admin",
                   Surname = "admin",
                   Username = "admin",
                   Email = "admin@gmail.com",
                   Password = "Admin123!",
                   Address = "Laze Teleckog 1, Novi Sad",
                   PhoneNumber = "060000000",
                   Role = Enums.Roles.Administrator,
                   IsBlocked = false,
                   IsMalicious = false,
                   AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiQWRtaW5pc3RyYXRvciIsIklkIjoiMjMiLCJleHAiOjE2MjUwMDIwNzl9.04bjhzsBD-9bYL-8CtuYMYTnNvrvxrReah_eF8-Hbro"
               }
           );

          //doctors
          builder.Entity<Doctor>().HasData(
              new Doctor
              {
                  Id = 2,
                  Name = "Ivan",
                  Surname = "Ivanovic",
                  Username = "ii",
                  Email = "ii@gmail.com",
                  Password = "Admin123!",
                  Address = "Laze Teleckog 1, Novi Sad",
                  PhoneNumber = "060000000",
                  Role = Enums.Roles.Doctor,
                  IsBlocked = false,
                  IsMalicious = false,
                  AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDAyODMyfQ.2eYr9v6qV-OnlBVAd23Y501ouDbhUbrKN_m5UnF2YJw",
                  Type = Enums.DoctorType.General,
                  MedicalAppointments = null,
                  Patients = null,
                  Recommendations = null,
              },
              new Doctor
              {
                  Id = 3,
                  Name = "Nena",
                  Surname = "Nenic",
                  Username = "nn",
                  Email = "nn@gmail.com",
                  Password = "Admin123!",
                  Address = "Laze Teleckog 1, Novi Sad",
                  PhoneNumber = "060000000",
                  Role = Enums.Roles.Doctor,
                  IsBlocked = false,
                  IsMalicious = false,
                  AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiIzIiwiZXhwIjoxNjI1MDc4MDgyfQ.FjZVW13Q2sQ4kpDqfPxS0LJDYDN1aVRc9B97FVZMAUA",
                  Type = Enums.DoctorType.General,
                  MedicalAppointments = null,
                  Patients = null,
                  Recommendations = null,
              },
              new Doctor
              {
                  Id = 4,
                  Name = "Dara",
                  Surname = "Daric",
                  Username = "dd",
                  Email = "dd@gmail.com",
                  Password = "Admin123!",
                  Address = "Laze Teleckog 1, Novi Sad",
                  PhoneNumber = "060000000",
                  Role = Enums.Roles.Doctor,
                  IsBlocked = false,
                  IsMalicious = false,
                  AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI0IiwiZXhwIjoxNjI1MDc4MTY3fQ.P-GUf_xNmiBTpzChik3CAuPx2FkWNGvtFaDMBNwI758",
                  Type = Enums.DoctorType.Specialist,
                  MedicalAppointments = null,
                  Patients = null,
                  Recommendations = null,
              },
              new Doctor
              {
                  Id = 5,
                  Name = "Vera",
                  Surname = "Veric",
                  Username = "vv",
                  Email = "vv@gmail.com",
                  Password = "Admin123!",
                  Address = "Laze Teleckog 1, Novi Sad",
                  PhoneNumber = "060000000",
                  Role = Enums.Roles.Doctor,
                  IsBlocked = false,
                  IsMalicious = false,
                  AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiRG9jdG9yIiwiSWQiOiI1IiwiZXhwIjoxNjI1MDc4NTEwfQ.9zWG0T0foHWCnuVGUofP-V-OEkPrAw9OfbJACog1m34",
                  Type = Enums.DoctorType.Specialist,
                  MedicalAppointments = null,
                  Patients = null,
                  Recommendations = null,
              }
          );

          //patients
          builder.Entity<Patient>().HasData(
             new Patient
             {
                 Id = 6,
                 Name = "Milica",
                 Surname = "Brujic",
                 Username = "mb",
                 Email = "mb@gmail.com",
                 Password = "Admin123!",
                 Address = "Laze Teleckog 1, Novi Sad",
                 PhoneNumber = "060000000",
                 Role = Enums.Roles.Patient,
                 IsBlocked = false,
                 IsMalicious = false,
                 AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiMjEiLCJleHAiOjE2MjQ5OTY4OTd9.nsZgWxwWSeB-RxQFfvLSr5ruLoXtCFsHwzyABiYL7i0",
                 MedicalAppointments = null,
                 PatientFeedbacks = null,
                 CancelledMedicalAppointments = 2,
                 GeneralDoctorId = 2,
                 Recommendations = null,
                 LastCanceledDate = DateTime.Now
             },
             new Patient
             {
                 Id = 7,
                 Name = "Ana",
                 Surname = "Antic",
                 Username = "aa",
                 Email = "aa@gmail.com",
                 Password = "Admin123!",
                 Address = "Laze Teleckog 1, Novi Sad",
                 PhoneNumber = "060000000",
                 Role = Enums.Roles.Patient,
                 IsBlocked = false,
                 IsMalicious = false,
                 AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiNyIsImV4cCI6MTYyNTA3ODY1NX0.2Fq9gQDhhfkVFg7_RoKTECIPZv9Aq0F1kmjWyHGB_Pw",
                 MedicalAppointments = null,
                 PatientFeedbacks = null,
                 CancelledMedicalAppointments = 1,
                 GeneralDoctorId = 2,
                 Recommendations = null,
                 LastCanceledDate = DateTime.Now
             },
             new Patient
             {
                 Id = 8,
                 Name = "Laza",
                 Surname = "Lazic",
                 Username = "ll",
                 Email = "ll@gmail.com",
                 Password = "Admin123!",
                 Address = "Laze Teleckog 1, Novi Sad",
                 PhoneNumber = "060000000",
                 Role = Enums.Roles.Patient,
                 IsBlocked = false,
                 IsMalicious = false,
                 AuthenticationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiUGF0aWVudCIsIklkIjoiOCIsImV4cCI6MTYyNTA3ODg0N30.U2S_GgPYTsermyI_8jiZMvSqR--uPNE0M5Qo_mk6SRw",
                 MedicalAppointments = null,
                 PatientFeedbacks = null,
                 CancelledMedicalAppointments = 1,
                 GeneralDoctorId = 3,
                 Recommendations = null,
                 LastCanceledDate = DateTime.Now
             }
         );

            //drugs
          builder.Entity<Drug>().HasData(
            new Drug
            {
                Id = 1,
                Name = "Brufen",
                Amount = 15,
            },
            new Drug
            {
                Id = 2,
                Name = "Aspirin",
                Amount = 20,
            },
            new Drug
            {
                Id = 3,
                Name = "Paracetamol",
                Amount = 30,
            }
        );

            //patientFeedbacks
            builder.Entity<PatientFeedback>().HasData(
               new PatientFeedback
               {
                   Id = 1,
                   Text = "Great Hospital.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 6
               },
               new PatientFeedback
               {
                   Id = 2,
                   Text = "Great service.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 7
               },
               new PatientFeedback
               {
                   Id = 3,
                   Text = "Great treetment.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 6
               },
               new PatientFeedback
               {
                   Id = 4,
                   Text = "Nice doctors.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 8
               },
               new PatientFeedback
               {
                   Id = 5,
                   Text = "Nice personel.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 8
               },
               new PatientFeedback
               {
                   Id = 6,
                   Text = "Polite workers.",
                   Date = DateTime.Now,
                   IsPosted = true,
                   PatientId = 6
               },
               new PatientFeedback
               {
                   Id = 7,
                   Text = "Bad service.",
                   Date = DateTime.Now,
                   IsPosted = false,
                   PatientId = 7
               },
               new PatientFeedback
               {
                   Id = 8,
                   Text = "Impolite doctors.",
                   Date = DateTime.Now,
                   IsPosted = false,
                   PatientId = 7
               },
               new PatientFeedback
               {
                   Id = 9,
                   Text = "Long waitings.",
                   Date = DateTime.Now,
                   IsPosted = false,
                   PatientId = 8
               }
           );
            
            //prescriptions
            builder.Entity<Prescription>().HasData(
               new Prescription
               {
                   Id = 1,
                   Text = "Take this medicine 3 times a week.",
                   PatientId = 6,
                   DoctorId = 2
               },
               new Prescription
               {
                   Id = 2,
                   Text = "Take this medicine 2 times a day.",
                   PatientId = 7,
                   DoctorId = 3
               },
               new Prescription
               {
                   Id = 3,
                   Text = "Take this medicine once a day.",
                   PatientId = 6,
                   DoctorId = 4
               }
            );
        }
    }
}
