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

        #region AdapterTests
        [Fact]
        public void Adapts_doctor_Dto_to_doctor()
        {
            //Arange
            DoctorDto doctorDto = CreateDoctorDto();

            //Act
            Doctor doctor = DoctorAdapter.DoctorDtoToDoctor(doctorDto);

            //Assert
            doctor.ShouldNotBeNull();
            doctor.ShouldBeOfType(typeof(Doctor));
        }

        [Fact]
        public void Adapts_doctor_to_doctor_Dto()
        {
            //Arange
            Doctor doctor = CreateDoctor();

            //Act
            DoctorDto doctorDto = DoctorAdapter.DoctorToDoctorDto(doctor);

            //Assert
            doctorDto.ShouldNotBeNull();
            doctorDto.ShouldBeOfType(typeof(DoctorDto));
        }
        #endregion AdapterTests

        #region GetGeneralDoctorsTests
        [Fact]
        public void Get_general_doctors()
        {
            //Arrange
            ArrangeForGetGeneralDoctors();

            //Act
            List<DoctorDto> _doctorDtos = _doctorService.GetGeneralDoctors();

            //Assert
            _doctorDtos.ShouldNotBeNull();
            _doctorDtos.Count.ShouldBeEquivalentTo(2);
        }

        [Fact]
        public void Get_general_doctors_controller()
        {
            //Arrange
            ArrangeForGetGeneralDoctors();

            //Act
            var actionResult = _doctorController.GetGeneralDoctors();

            //Assert
            ((actionResult as OkObjectResult).Value as List<DoctorDto>).ShouldBeEquivalentTo(CreateGeneralDoctorDtos());
        }
        #endregion GetGeneralDoctorsTests

        #region HelperFunctions
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
        private DoctorDto CreateDoctorDto()
        {
            return new DoctorDto
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = "Patient",
                IsBlocked = false,
                IsMalicious = false,
                Type = "General"
            };
        }
        private Doctor CreateDoctor()
        {
            return new Doctor
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.General
            };
        }
        private List<Doctor> CreateDoctors()
        {
            _doctors.Add(new Doctor
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.General
            });

            _doctors.Add(new Doctor
            {
                Id = 2,
                Name = "Ivan",
                Surname = "Ivanovic",
                Username = "ii",
                Email = "ii@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.General
            });

            _doctors.Add(new Doctor
            {
                Id = 1,
                Name = "Ana",
                Surname = "Anic",
                Username = "aa",
                Email = "aa@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.Specialist
            });

            _doctors.Add(new Doctor
            {
                Id = 2,
                Name = "Stefan",
                Surname = "Stefanovic",
                Username = "ss",
                Email = "ss@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.Specialist
            });

            return _doctors;
        }
       
        private List<DoctorDto> CreateGeneralDoctorDtos()
        {
            _doctors.GetRange(0, 2).ForEach(doctor => _doctorDtos.Add(DoctorAdapter.DoctorToDoctorDto(doctor)));

            return _doctorDtos;
        }

        private void ArrangeForGetGeneralDoctors()
        {
            _stubDoctorRepository.Setup(x => x.GetGeneralDoctors()).Returns(CreateDoctors().FindAll(doctor => doctor.Type.Equals(DoctorType.General)));
            _doctorService = new DoctorService(_stubDoctorRepository.Object);
            _doctorController = new DoctorController(_doctorService);
        }
        #endregion HelperFunctions
    }
}
