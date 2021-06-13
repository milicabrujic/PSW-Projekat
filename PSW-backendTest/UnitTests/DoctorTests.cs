using Microsoft.AspNetCore.Mvc;
using Moq;
using PSW_backend.Adapters;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using PSW_backend.Repositories;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PSW_backendTest.UnitTests
{
    public class DoctorTests
    {
        #region Variables
        private readonly Mock<IDoctorRepository> _stubDoctorRepository;
        private DoctorService _doctorService;
        private DoctorController _doctorController;
        private List<Doctor> _doctors;
        private List<DoctorDto> _doctorDtos;
        private readonly Mock<IPatientRepository> _stubPatientRepository;
        private PatientService _patientService;
        private PatientController _patientController;
        private List<Patient> _patients;
        #endregion Variables
        public DoctorTests()
        {
            _stubDoctorRepository = new Mock<IDoctorRepository>();
            _stubPatientRepository = new Mock<IPatientRepository>();
            _patients = new List<Patient>();
            _doctors = new List<Doctor>();
            _doctorDtos = new List<DoctorDto>();
        }

        [Fact]
        public void Finds__patient_general_doctor()
        {
            Patient patient = CreatePatient();
            //Arange
        //    _stubPatientRepository.Setup(x => x.GetPatientByUsername(patient.Username)).Returns(CreatePatients().Find(patient => patient.Username == patient.Username));
            _stubDoctorRepository.Setup(x => x.GetGeneralDoctorByPatientUsername(patient.Username)).Returns(CreateDoctors().Find(doctor => doctor.Id == patient.GeneralDoctorId));
            _patientService = new PatientService(_stubPatientRepository.Object);

            //Act
            Doctor doctor = _doctorService.getGeneralDoctor(patient.Username);


            //Assert
            doctor.ShouldNotBeNull();
        }
        private Patient CreatePatient()
        {
            return new Patient
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
                CancelledMedicalAppointments = 2,
                GeneralDoctorId = 1
            };
        }
        private List<Patient> CreatePatients()
        {
            _patients.Add(new Patient
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
                CancelledMedicalAppointments = 2,
                GeneralDoctorId = 3
            });

            _patients.Add(new Patient
            {
                Id = 2,
                Name = "Ivan",
                Surname = "Ivanovic",
                Username = "ii",
                Email = "ii@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
                CancelledMedicalAppointments = 1,
                GeneralDoctorId = 3
            });

            return _patients;
        }

        private List<Doctor> CreateDoctors()
        {
            _doctors.Add(new Doctor
            {
                Id = 3,
                Name = "Doctor",
                Surname = "Doctor",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.General
            });

            _doctors.Add(new Doctor
            {
                Id = 4,
                Name = "Doca2",
                Surname = "Ivanovic",
                Username = "ii",
                Email = "ii@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.Specialist
            });

            return _doctors;
        }
    }
}
