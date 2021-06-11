﻿using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using PSW_backend.Adapters;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using PSW_backend.Services;
using Shouldly;
using Xunit;
using System.Linq;
using PSW_backend.Repositories;

namespace PSW_backendTest.UnitTests
{
    public class MedicalAppointmentTests
    {
        #region Variables
        private readonly Mock<IMedicalAppointmentRepository> _stubMedicalAppointmentRepository;
        private readonly Mock<IDoctorRepository> _stubDoctorRepository;
        private MedicalAppointmentService _medicalAppointmentService;
        private MedicalAppointmentController _medicalAppointmentController;
        private List<MedicalAppointment> _medicalAppointments;
        private List<Doctor> _doctors;
        private List<MedicalAppointmentDto> _medicalAppointmentDtos;
        #endregion Variables
        public MedicalAppointmentTests()
        {
            _stubMedicalAppointmentRepository = new Mock<IMedicalAppointmentRepository>();
            _stubDoctorRepository = new Mock<IDoctorRepository>();
            _medicalAppointments = new List<MedicalAppointment>();
            _doctors = new List<Doctor>();
            _medicalAppointmentDtos = new List<MedicalAppointmentDto>();
        }
        #region AdapterTests
        [Fact]
        public void Adapts_medicalAppointment_Dto_to_medicalAppointment()
        {
            //Arange
            MedicalAppointmentDto medicalAppointmentDto = CreateMedicalAppointmentDto();

            //Act
            MedicalAppointment medicalAppointment = MedicalAppointmentAdapter.MedicalAppointmentDtoToMedicalsAppointment(medicalAppointmentDto);

            //Assert
            medicalAppointment.ShouldNotBeNull();
            medicalAppointment.ShouldBeOfType(typeof(MedicalAppointment));
        }

        [Fact]
        public void Adapts_medicalAppointment_to_medicalAppointment_Dto()
        {
            //Arange
            MedicalAppointment medicalAppointment = CreateMedicalAppointment();

            //Act
            MedicalAppointmentDto medicalAppointmentDto = MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentDto(medicalAppointment);

            //Assert
            medicalAppointmentDto.ShouldNotBeNull();
            medicalAppointmentDto.ShouldBeOfType(typeof(MedicalAppointmentDto));
        }
        #endregion AdapterTests
        #region MedicalAppointmentTests
        [Fact]
        public void Find_that_date_already_exists()
        {
            Doctor GeneralDoctor = CreateGeneralDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(GeneralDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == GeneralDoctor.Id).ToList());
            _medicalAppointmentService =new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            bool DateExists = _medicalAppointmentService.CheckDateEquality(GeneralDoctor, new DateTime(2021, 10, 10, 10, 10, 00));
            DateExists.ShouldBeTrue();
        }
        [Fact]
        public void Find_that_date_already_exists_when_doctor_specialist()
        {
            Doctor SpecialistDoctor = CreateSpecialistDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            bool DateExists = _medicalAppointmentService.CheckDateEquality(SpecialistDoctor, new DateTime(2021, 10, 10, 10, 10, 00));
            DateExists.ShouldBeTrue();
        }
        [Fact]
        public void Find_that_date_does_not_already_exists()
        {
            Doctor GeneralDoctor = CreateGeneralDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(GeneralDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == GeneralDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            bool DateExists = _medicalAppointmentService.CheckDateEquality(GeneralDoctor, new DateTime(2021, 12, 10, 10, 10, 00));
            DateExists.ShouldBeFalse();
        }
        [Fact]
        public void Find_that_date_does_not_already_exists_when_doctor_specialist()
        {
            Doctor SpecialistDoctor = CreateSpecialistDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            bool DateExists = _medicalAppointmentService.CheckDateEquality(SpecialistDoctor, new DateTime(2021, 12, 10, 10, 10, 00));
            DateExists.ShouldBeFalse();
        }
        [Fact]
        public void Find_dateTime_for_appointment_expected_one() {
            Doctor GeneralDoctor = CreateGeneralDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(GeneralDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == GeneralDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            DateTime currentDate = new DateTime(2021, 12, 10, 10, 10, 00);
            DateTime returnedDate = _medicalAppointmentService.FindDoctorTime(GeneralDoctor, currentDate);
            returnedDate.ShouldBeEquivalentTo(currentDate);
        }
        [Fact]
        public void Find_dateTime_for_appointment_changed()
        {
            Doctor GeneralDoctor = CreateGeneralDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(GeneralDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == GeneralDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);
            DateTime returnedDate = _medicalAppointmentService.FindDoctorTime(GeneralDoctor, currentDate);
            returnedDate.ShouldNotBeSameAs(currentDate);
        }
        [Fact]
        public void New_doctor_is_null()
        {
            List<Doctor> specialists = CreateSpecialistDoctors();
            _stubDoctorRepository.Setup(x => x.DoctorSpecialists()).Returns(CreateSpecialistDoctors().Where(doctor => doctor.Type.Equals(PSW_backend.Enums.DoctorType.Specialist)).ToList());
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(3)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == 3).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);
            Doctor doctor = _medicalAppointmentService.ChangeDoctor(currentDate);
            doctor.ShouldBeNull();

        }
        [Fact]
        public void Doctor_specialist_is_same()
        {
            Doctor SpecialistDoctor = CreateSpecialistDoctor();
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            DateTime currentDate = new DateTime(2021, 11, 10, 10, 10, 00);
            Doctor doctor = _medicalAppointmentService.FindDoctorSpecialist(SpecialistDoctor, currentDate);
            SpecialistDoctor.Id.ShouldBeEquivalentTo(doctor.Id);          
        }
        [Fact]
        public void Doctor_specialist_is_null()
        {
            Doctor SpecialistDoctor = CreateSpecialistDoctor();
            _stubDoctorRepository.Setup(x => x.DoctorSpecialists()).Returns(CreateSpecialistDoctors().Where(doctor => doctor.Type.Equals(PSW_backend.Enums.DoctorType.Specialist)).ToList());
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);
            Doctor doctor = _medicalAppointmentService.FindDoctorSpecialist(SpecialistDoctor, currentDate);
            doctor.ShouldBeNull();
        }
        #endregion  MedicalAppointmentTests
        #region HelperFunctions
        private MedicalAppointmentDto CreateMedicalAppointmentDto()
        {
            return new MedicalAppointmentDto
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 2,
                Date = new DateTime(2021, 10, 10, 10, 10, 00) 
             };
        }
        private MedicalAppointment CreateMedicalAppointment()
        {
            return new MedicalAppointment
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 2,
                Date = new DateTime(2021, 10, 10, 10, 10, 00)
            };
        }
        private Doctor CreateGeneralDoctor()
        {
            return new Doctor
            {
                Id = 2,
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
        private Doctor CreateSpecialistDoctor()
        {
            return new Doctor
            {
                Id = 3,
                Name = "Milica",
                Surname = "Glavas",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Doctor,
                IsBlocked = false,
                IsMalicious = false,
                Type = DoctorType.Specialist
            };
        }
        private List<MedicalAppointment> CreateAppintments()
        {
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 2,
                Date = new DateTime(2021, 10, 10, 10, 10, 00)
            });

            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 2,
                PatientId = 1,
                DoctorId = 3,
                Date = new DateTime(2021, 10, 10, 10, 10, 00)
            });
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 2,
                PatientId = 1,
                DoctorId = 3,
                Date = new DateTime(2021, 12, 10, 10, 10, 00)
            });
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 3,
                PatientId = 1,
                DoctorId = 4,
                Date = new DateTime(2021, 10, 10, 10, 10, 00)
            });

            return _medicalAppointments;
        }
        private List<Doctor> CreateSpecialistDoctors()
        {
            _doctors.Add(new Doctor
            {
                Id = 3,
                Name = "Milica",
                Surname = "Glavas",
                Username = "mb",
                Email = "mb@gmail.com",
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

        #endregion HelperFunctions
    }
}
