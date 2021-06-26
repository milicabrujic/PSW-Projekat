using Microsoft.AspNetCore.Mvc;
using Moq;
using PSW_backend.Adapters;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PSW_backendTest.UnitTests
{
    public class PatientTests
    {
        #region Variables
        private readonly Mock<IPatientRepository> _stubPatientRepository;
        private PatientService _patientService;
        private PatientController _patientController;
        private List<Patient> _patients;
        private List<PatientDto> _patientDtos;
        #endregion Variables

        public PatientTests()
        {
            _stubPatientRepository = new Mock<IPatientRepository>();

            _patients = new List<Patient>();
            _patientDtos = new List<PatientDto>();
        }

        #region AdapterTests
        [Fact]
        public void Adapts_patient_Dto_to_patient()
        {
            //Arange
            PatientDto patientDto = CreatePatientDto();

            //Act
            Patient patient = PatientAdapter.PatientDtoToPatient(patientDto);

            //Assert
            patient.ShouldNotBeNull();
            patient.ShouldBeOfType(typeof(Patient));
        }

        [Fact]
        public void Adapts_patient_to_patient_Dto()
        {
            //Arange
            Patient patient = CreatePatient();

            //Act
            PatientDto patientDto = PatientAdapter.PatientToPatientDto(patient);

            //Assert
            patientDto.ShouldNotBeNull();
            patientDto.ShouldBeOfType(typeof(PatientDto));
        }
        #endregion AdapterTests

        #region PatientRegistrationTests
        [Theory]
        [InlineData("mb", "aa")]
        [InlineData("aa", "mb@gmail.com")]
        [InlineData("mb", "mb@gmail.com")]
        public void Finds_that_patient_already_exists(string username, string email)
        {
            //Arange
            ArrangeForCheckingIfUserExistsTests(username, email);

            //Act
            bool PatientExists = _patientService.CheckIfPatientExists(username, email); 

            //Assert
            PatientExists.ShouldBeTrue();
        }

        [Fact]
        public void Finds_that_patient_does_not_already_exist()
        {
            //Arange
            string username = "aa";
            string email = "aa@gmail.com";
            ArrangeForCheckingIfUserExistsTests(username, email);

            //Act
            bool PatientExists = _patientService.CheckIfPatientExists(username, email);

            //Assert
            PatientExists.ShouldBeFalse();
        }
       
        [Fact]
        public void Successfully_generates_authentication_token()
        {
            //Arange
            _patientService = new PatientService(_stubPatientRepository.Object);

            //Act
            string authenticationToken = _patientService.GenerateAuthenticationToken();

            //Assert
            authenticationToken.ShouldNotBeNull();
            authenticationToken.ShouldBeOfType<string>();
        }

        [Fact]
        public void Register_patient_controller()
        {
            //Arange
            PatientDto patientDto = CreatePatientDto();
            _patientService = new PatientService(_stubPatientRepository.Object);
            _patientController = new PatientController(_patientService);

            //Act
            var actionResult = _patientController.RegisterPatient(patientDto);

            //Assert
            ((actionResult as OkObjectResult).Value as PatientDto).ShouldBeEquivalentTo(patientDto);
        }
        #endregion PatientRegistrationTests
        
        #region PatientBlockingTests
        [Fact]
        public void Gets_all_malicious_patient_service()
        {
            //Arange
            ArrangeForGetMaliciousPatients();

            //Act
            List<PatientDto> patient = _patientService.GetMaliciousPatients();

            //Assert
            patient.ShouldNotBeNull();
            patient.ShouldNotBeEmpty();
            patient.Count.ShouldBeEquivalentTo(2);
        }
        [Fact]
        public void Checks_if_patient_is_block()
        {
            //Arange
            PatientDto patientMalicious = CreatePatientDto();
            Patient patientTemp = CreatePatient();

            _stubPatientRepository.Setup(x => x.BlockPatient(patientMalicious.Username)).Returns(CreateMaliciousPatients().Find(patient => patient.Username == patientMalicious.Username));
            _patientService = new PatientService(_stubPatientRepository.Object);

            //Act
            PatientDto patient = _patientService.BlockPatient(patientMalicious.Username);
            Console.WriteLine(patient.IsBlocked);
            //Assert
            patientTemp.IsBlocked.ShouldNotBe(patient.IsBlocked);
        }

        [Fact]
        public void Compares_dates_over_one_month()
        {
            //Arange
            DateTime today = DateTime.Now;
            DateTime overOneMonthAgo = new DateTime(2020, 1, 1, 0, 0, 0);
            _patientService = new PatientService(_stubPatientRepository.Object);
            //Act
            bool overOneMonth = _patientService.CompareDates(overOneMonthAgo, today);

            //Assert
            overOneMonth.ShouldBeTrue();
        }

        [Fact]
        public void Compares_dates_under_one_month()
        {
            //Arange
            DateTime today = DateTime.Now;
            DateTime overOneMonthAgo = new DateTime(2021, 6, 25, 0, 0, 0);
            _patientService = new PatientService(_stubPatientRepository.Object);
            //Act
            bool overOneMonth = _patientService.CompareDates(overOneMonthAgo, today);

            //Assert
            overOneMonth.ShouldBeFalse();
        }
        [Fact]
        public void Patient_updated_to_malicious()
        {
            //Arange
            Patient maliciousPatient = CreatePatient();
            _patientService = new PatientService(_stubPatientRepository.Object);
            //Act
            Patient patient = _patientService.UpdateMalitiousPatient(false, maliciousPatient);

            //Assert
            patient.IsMalicious.ShouldBeTrue();
        }
        [Fact]
        public void Patient_cancelled_appointments_updated_to_one()
        {
            //Arange
            Patient maliciousPatient = CreatePatient();
            _stubPatientRepository.Setup(x => x.GetPatientByUsername(maliciousPatient.Username)).Returns(CreatePatient);
            _patientService = new PatientService(_stubPatientRepository.Object);
            //Act
            Patient patient = _patientService.UpdateMalitiousPatient(true, maliciousPatient);

            //Assert
            patient.CancelledMedicalAppointments.ShouldBe(1);
        }
        #endregion PatientBlockingTests

        #region HelperFunctions
        private PatientDto CreatePatientDto()
        {
            return new PatientDto
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
               CancelledMedicalAppointments = 2,
               GeneralDoctorId = 1
            };
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
        private Patient CreateMaliciousPatient()
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
                GeneralDoctorId = 1
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
                GeneralDoctorId = 1
            });

            return _patients;
        }
        private List<PatientDto> CreateMaliciousPatientsDto()
        {
            _patientDtos.Add(new PatientDto
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                // Role = Roles.Patient,
                IsBlocked = true,
                IsMalicious = true,
                CancelledMedicalAppointments = 2,
                GeneralDoctorId = 1
            });

            _patientDtos.Add(new PatientDto
            {
                Id = 2,
                Name = "Ivan",
                Surname = "Ivanovic",
                Username = "ii",
                Email = "ii@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                // Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = true,
                CancelledMedicalAppointments = 1,
                GeneralDoctorId = 1
            });

            return _patientDtos;
        }
        private List<Patient> CreateMaliciousPatients()
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
                IsBlocked = true,
                IsMalicious = true,
                CancelledMedicalAppointments = 2,
                GeneralDoctorId = 1
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
                IsMalicious = true,
                CancelledMedicalAppointments = 1,
                GeneralDoctorId = 1
            });

            return _patients;
        }
        private void ArrangeForCheckingIfUserExistsTests(string username, string email)
        {
            _stubPatientRepository.Setup(x => x.GetPatientByUsername(username)).Returns(CreatePatients().Find(patient => patient.Username == username));
            _stubPatientRepository.Setup(x => x.GetPatientByEmail(email)).Returns(CreatePatients().Find(patient => patient.Email == email));
            _patientService = new PatientService(_stubPatientRepository.Object);
        }

        private void ArrangeForGetMaliciousPatients()
        {
            _patients = CreateMaliciousPatients();
            _patientDtos = CreateMaliciousPatientsDto();
            _stubPatientRepository.Setup(x => x.GetMaliciousPatients()).Returns(_patients);
            _patientService = new PatientService(_stubPatientRepository.Object);
            _patientController = new PatientController(_patientService);
        }
        #endregion HelperFunctions
    }
}
