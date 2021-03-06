using PSW_backend.Repositories.Interfaces;
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
using Microsoft.AspNetCore.Mvc;

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
        [Fact]
        public void Adapts_medicalAppointment_history_Dto_to_medicalAppointment()
        {
            //Arange
            MedicalAppointmentHistoryDto medicalAppointmentDto = CreateMedicalAppointmentHistoryDto();

            //Act
            MedicalAppointment medicalAppointment = MedicalAppointmentAdapter.MedicalAppointmentHistoryDtoToMedicalsAppointment(medicalAppointmentDto);

            //Assert
            medicalAppointment.ShouldNotBeNull();
            medicalAppointment.ShouldBeOfType(typeof(MedicalAppointment));
        }
        [Fact]
        public void Adapts_medicalAppointment_to_medicalAppointment_history_Dto()
        {
            //Arange
            MedicalAppointment medicalAppointment = CreateMedicalAppointment();

            //Act
            MedicalAppointmentHistoryDto medicalAppointmentHistoryDto = MedicalAppointmentAdapter.MedicalAppointmentToMedicalAppointmentHistoryDto(medicalAppointment);

            //Assert
            medicalAppointmentHistoryDto.ShouldNotBeNull();
            medicalAppointmentHistoryDto.ShouldBeOfType(typeof(MedicalAppointmentHistoryDto));
        }
        #endregion AdapterTests
        #region MedicalAppointmentTests
        [Fact]
        public void Find_that_date_already_exists()
        {
            //Arange
            Doctor GeneralDoctor = CreateGeneralDoctor();

            ArrangeForTestingDateTime(GeneralDoctor.Id);
            
            //Act
            bool DateExists = _medicalAppointmentService.CheckDateEquality(GeneralDoctor, new DateTime(2021, 10, 10, 10, 10, 00));

            //Assert
            DateExists.ShouldBeTrue();
        }
        [Fact]
        public void Find_that_date_already_exists_when_doctor_specialist()
        {
            //Arange
            Doctor SpecialistDoctor = CreateSpecialistDoctor();

            ArrangeForTestingDateTime(SpecialistDoctor.Id);

            //Act
            bool DateExists = _medicalAppointmentService.CheckDateEquality(SpecialistDoctor, new DateTime(2021, 10, 10, 10, 10, 00));

            //Assert
            DateExists.ShouldBeTrue();
        }
        [Fact]
        public void Find_that_date_does_not_already_exists()
        {
            //Arange
            Doctor GeneralDoctor = CreateGeneralDoctor();

            ArrangeForTestingDateTime(GeneralDoctor.Id);
            
            //Act
            bool DateExists = _medicalAppointmentService.CheckDateEquality(GeneralDoctor, new DateTime(2021, 12, 10, 10, 10, 00));

            //Assert
            DateExists.ShouldBeFalse();
        }
        [Fact]
        public void Find_that_date_does_not_already_exists_when_doctor_specialist()
        {
            //Arange
            Doctor SpecialistDoctor = CreateSpecialistDoctor();

            ArrangeForTestingDateTime(SpecialistDoctor.Id);
            
            //Act
            bool DateExists = _medicalAppointmentService.CheckDateEquality(SpecialistDoctor, new DateTime(2021, 12, 12, 10, 10, 00));

            //Assert
            DateExists.ShouldBeFalse();
        }
        [Fact]
        public void Find_dateTime_for_appointment_expected_one() {
            //Arange
            Doctor GeneralDoctor = CreateGeneralDoctor();

            ArrangeForTestingDateTime(GeneralDoctor.Id);
                
            DateTime currentDate = new DateTime(2021, 12, 10, 10, 10, 00);
            
            //Act
            DateTime returnedDate = _medicalAppointmentService.FindDoctorTime(GeneralDoctor, currentDate);

            //Assert
            returnedDate.ShouldBeEquivalentTo(currentDate);
        }
        [Fact]
        public void Find_dateTime_for_appointment_changed()
        {
            //Arange
            Doctor GeneralDoctor = CreateGeneralDoctor();

            ArrangeForTestingDateTime(GeneralDoctor.Id);

            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);

            //Act
            DateTime returnedDate = _medicalAppointmentService.FindDoctorTime(GeneralDoctor, currentDate);

            //Assert
            returnedDate.ShouldNotBeSameAs(currentDate);
        }
        [Fact]
        public void New_doctor_is_null()
        {
            //Arange
            List<Doctor> specialists = CreateSpecialistDoctors();

            _stubDoctorRepository.Setup(x => x.DoctorSpecialists()).Returns(CreateSpecialistDoctors().Where(doctor => doctor.Type.Equals(PSW_backend.Enums.DoctorType.Specialist)).ToList());
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(3)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == 3).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
           
            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);

            //Act
            Doctor doctor = _medicalAppointmentService.ChangeDoctor(currentDate);

            //Assert
            doctor.ShouldBeNull();

        }
        [Fact]
        public void Doctor_specialist_is_same()
        {
            //Arange
            Doctor SpecialistDoctor = CreateSpecialistDoctor();

            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
           
            DateTime currentDate = new DateTime(2021, 11, 10, 10, 10, 00);

            //Act
            Doctor doctor = _medicalAppointmentService.FindDoctorSpecialist(SpecialistDoctor, currentDate);

            //Assert
            SpecialistDoctor.Id.ShouldBeEquivalentTo(doctor.Id);          
        }
        [Fact]
        public void Doctor_specialist_is_null()
        {   //uraditi i da vrati drugog doktora
            //Arange
            Doctor SpecialistDoctor = CreateSpecialistDoctor();

            _stubDoctorRepository.Setup(x => x.DoctorSpecialists()).Returns(CreateSpecialistDoctors().Where(doctor => doctor.Type.Equals(PSW_backend.Enums.DoctorType.Specialist)).ToList());
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(SpecialistDoctor.Id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == SpecialistDoctor.Id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
           
            DateTime currentDate = new DateTime(2021, 10, 10, 10, 10, 00);

            //Act
            Doctor doctor = _medicalAppointmentService.FindDoctorSpecialist(SpecialistDoctor, currentDate);

            //Assert
            doctor.ShouldBeNull();
        }
        [Fact]
        public void Get_expected_appointment_priority_doctor()
        {
            //Arange
            MedicalAppointmentDto medicalAppointmentDto = CreateMedicalAppointmentDto();
            medicalAppointmentDto.Date = new DateTime(2021, 11, 13, 10, 10, 00);

            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(medicalAppointmentDto.DoctorId)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == medicalAppointmentDto.DoctorId).ToList());
            _stubDoctorRepository.Setup(x => x.FindById(medicalAppointmentDto.DoctorId)).Returns(CreateSpecialistDoctors().Find(doctor => doctor.Id.Equals(medicalAppointmentDto.DoctorId)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

            //Act
            MedicalAppointmentDto foundedAppointment = _medicalAppointmentService.FindAppointment(medicalAppointmentDto, "doctor");

            //Assert
            medicalAppointmentDto.Date.ShouldBe(foundedAppointment.Date);
            
        }
        [Fact]
        public void Get_differente_time_appointment_priority_doctor()
        {
            //Arange
            MedicalAppointmentDto medicalAppointmentDto = CreateMedicalAppointmentDto();
            MedicalAppointmentDto medicalAppointmentDtoForCheck = CreateMedicalAppointmentDto();

            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(medicalAppointmentDto.DoctorId)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == medicalAppointmentDto.DoctorId).ToList());
            _stubDoctorRepository.Setup(x => x.FindById(medicalAppointmentDto.DoctorId)).Returns(CreateSpecialistDoctors().Find(doctor => doctor.Id.Equals(medicalAppointmentDto.DoctorId)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            
            //Act
            MedicalAppointmentDto foundedAppointment = _medicalAppointmentService.FindAppointment(medicalAppointmentDto, "doctor");

            //Assert
            medicalAppointmentDtoForCheck.Date.ShouldNotBe(foundedAppointment.Date);

        }
   
        [Fact]
        public void Get_active_doctor_appointments_controller()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(dto.DoctorId)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId.Equals(dto.DoctorId)).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            _medicalAppointmentController = new MedicalAppointmentController(_medicalAppointmentService);

            //Act
            var actionResult = _medicalAppointmentController.GetDoctorActiveAppointments(dto.DoctorId);

            //Assert
            ((actionResult as OkObjectResult).Value as List<MedicalAppointment>).Count.ShouldBeGreaterThan(0);

        }
        [Fact]
        public void Cancel_medical_appointment_controller()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();

            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.CancelMedicalAppointment(dto.Id)).Returns(CreateAppintments().Find(appointment => appointment.Id.Equals(dto.Id)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            _medicalAppointmentController = new MedicalAppointmentController(_medicalAppointmentService);

            //Act
            var actionResult = _medicalAppointmentController.CancelMedicalAppointment(dto.Id);

            //Assert
            ((actionResult as OkObjectResult).Value as MedicalAppointmentDto).ShouldBeEquivalentTo(dto);

        }
        [Fact]
        public void End_medical_appointment_controller()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            MedicalAppointment appointment = CreateMedicalAppointment();

            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.EndMedicalAppointment(dto.Id)).Returns(CreateAppintments().Find(appointment => appointment.Id.Equals(dto.Id)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);
            _medicalAppointmentController = new MedicalAppointmentController(_medicalAppointmentService);

            //Act
            var actionResult = _medicalAppointmentController.EndMedicalAppointment(dto);

            //Assert
            ((actionResult as OkObjectResult).Value as MedicalAppointmentDto).ShouldBeEquivalentTo(dto);

        }
        [Fact]
        public void End_medical_appointment_service()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.EndMedicalAppointment(dto.Id)).Returns(CreateAppintments().Find(appointment => appointment.Id.Equals(dto.Id)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

            //Act
            MedicalAppointmentDto appointmentDto = _medicalAppointmentService.EndMedicalAppointment(dto.Id);

            //Assert
           appointmentDto.Id.ShouldBeEquivalentTo(1);

        }
        [Fact]
        public void Cancel_medical_appointment_service()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.CancelMedicalAppointment(dto.Id)).Returns(CreateAppintments().Find(appointment => appointment.Id.Equals(dto.Id)));
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

            //Act
            MedicalAppointmentDto appointmentDto = _medicalAppointmentService.CancelMedicalAppointment(dto.Id);

            //Assert
            appointmentDto.Id.ShouldBeEquivalentTo(1);

        }
        [Fact]
        public void Get_doctor_active_appointments()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(dto.DoctorId)).Returns(CreateAppintments().Where(appointment => appointment.Status == MedicalAppointmentStatus.Active).ToList);
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

            //Act
            List<MedicalAppointment> medicalAppointments = _medicalAppointmentService.GetDoctorActiveAppointments(dto.DoctorId);

            //Assert
            medicalAppointments.Count.ShouldBeEquivalentTo(5);

        }
        [Fact]
        public void Get_patient_appointments()
        {
            MedicalAppointmentDto dto = CreateMedicalAppointmentDto();
            //Arange
            _stubMedicalAppointmentRepository.Setup(x => x.GetPatientMedicalAppointments(dto.PatientId)).Returns(CreateAppintments().Where(appointment => appointment.PatientId == dto.PatientId).ToList);
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

            //Act
            List<MedicalAppointmentHistoryDto> medicalAppointments = _medicalAppointmentService.GetPatientAppointments(dto.PatientId);

            //Assert
            medicalAppointments.Count.ShouldBeEquivalentTo(5);

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
        private MedicalAppointmentHistoryDto CreateMedicalAppointmentHistoryDto()
        {
            return new MedicalAppointmentHistoryDto
            {
                Id = 1,
                PatientId = 1,
                DoctorId = 2,
                Date = new DateTime(2021, 10, 10, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active,
                Cancelled = false
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
                Date = new DateTime(2021, 10, 10, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active
            });;
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 2,
                PatientId = 1,
                DoctorId = 2,
                Date = new DateTime(2021, 11, 11, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active
            });
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 3,
                PatientId = 1,
                DoctorId = 3,
                Date = new DateTime(2021, 10, 10, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active
            });
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 4,
                PatientId = 1,
                DoctorId = 3,
                Date = new DateTime(2021, 12, 10, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active
            });
            _medicalAppointments.Add(new MedicalAppointment
            {
                Id = 5,
                PatientId = 1,
                DoctorId = 4,
                Date = new DateTime(2021, 10, 10, 10, 10, 00),
                Status = MedicalAppointmentStatus.Active
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
            _doctors.Add(new Doctor
            {
                Id = 2,
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
                Type = DoctorType.General
            });



            return _doctors;
        }

        private void ArrangeForTestingDateTime(int id)
        {
            _stubMedicalAppointmentRepository.Setup(x => x.GetDoctorAppointments(id)).Returns(CreateAppintments().Where(appointment => appointment.DoctorId == id).ToList());
            _medicalAppointmentService = new MedicalAppointmentService(_stubMedicalAppointmentRepository.Object, _stubDoctorRepository.Object);

        }
        #endregion HelperFunctions
    }
}
