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

namespace PSW_backendTest.UnitTests
{
    public class MedicalAppointmentTests
    {
        #region Variables
        private readonly Mock<IMedicalAppointmentRepository> _stubMedicalAppointmentRepository;
        private MedicalAppointmentService _medicalAppointmentService;
        private MedicalAppointmentController _medicalAppointmentController;
        private List<MedicalAppointment> _medicalAppointments;
        private List<MedicalAppointmentDto> _medicalAppointmentDtos;
        #endregion Variables
        public MedicalAppointmentTests()
        {
            _stubMedicalAppointmentRepository = new Mock<IMedicalAppointmentRepository>();

            _medicalAppointments = new List<MedicalAppointment>();
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
        #endregion HelperFunctions
    }
}
