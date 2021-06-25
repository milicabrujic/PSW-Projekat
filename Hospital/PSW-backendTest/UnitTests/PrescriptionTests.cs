using Microsoft.AspNetCore.Mvc;
using Moq;
using PSW_backend.Adapters;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
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
    public class PrescriptionTests
    {
        #region Variables
        private readonly Mock<IPrescriptionRepository> _stubPrescriptionRepository;
        private PrescriptionService _prescriptionService;
        private PrescriptionController _prescriptionController;
        private List<Prescription> _prescriptions;
        private List<PrescriptionDto> _prescriptionDTOs;
        #endregion Variables

        public PrescriptionTests()
        {
            _stubPrescriptionRepository = new Mock<IPrescriptionRepository>();

            _prescriptions = new List<Prescription>();
            _prescriptionDTOs = new List<PrescriptionDto>();
        }

        #region AdapterTests
        [Fact]
        public void Adapts_prescription_DTO_to_prescription()
        {
            //Arange
            PrescriptionDto prescriptionDTO = CreatePrescriptionDto();

            //Act
            Prescription prescription = PrescriptionAdapter.PrescriptionDtoToPrescription(prescriptionDTO);

            //Assert
            prescription.ShouldNotBeNull();
            prescription.ShouldBeOfType(typeof(Prescription));
        }

        [Fact]
        public void Adapts_patient_feedback_to_patient_feedback_DTO()
        {
            //Arange
            Prescription prescription = CreatePrescription();

            //Act
            PrescriptionDto prescriptionDTO = PrescriptionAdapter.PrescriptionToPrescriptionDto(prescription);

            //Assert
            prescriptionDTO.ShouldNotBeNull();
            prescriptionDTO.ShouldBeOfType(typeof(PrescriptionDto));
        }
        #endregion AdapterTests

        #region CreatePrescription
        [Fact]
        public void Checks_if_prescription_is_created()
        {
            //Arange
            _stubPrescriptionRepository.Setup(x => x.SaveNewPrescription(CreatePrescription()));
            _prescriptionService = new PrescriptionService(_stubPrescriptionRepository.Object);

            //Act
            PrescriptionDto prescriptionDTO = _prescriptionService.SaveNewPrescription(CreatePrescriptionDto());

            //Assert
            prescriptionDTO.ShouldNotBeNull();
            _stubPrescriptionRepository.Verify(x => x.SaveNewPrescription(It.IsAny<Prescription>()), Times.Once);
        }

        [Fact]
        public void Checks_if_prescription_is_created_controller()
        {
            //Arange
            PrescriptionDto prescriptionDto = CreatePrescriptionDto();
            _stubPrescriptionRepository.Setup(x => x.SaveNewPrescription(CreatePrescription()));
            _prescriptionService = new PrescriptionService(_stubPrescriptionRepository.Object);
            _prescriptionController = new PrescriptionController(_prescriptionService);

            //Act
            var actionResult = _prescriptionController.SaveNewPrescription(prescriptionDto);

            //Assert
            ((actionResult as OkObjectResult).Value as PrescriptionDto).ShouldBeEquivalentTo(prescriptionDto);
        }
        #endregion CreatePrescription

        #region HelperFunctions
        private List<Prescription> CreatePrescriptions()
        {
            _prescriptions.Add(new Prescription
            {
                Id = 1,
                Text = "Migrenes prescription.",
                PatientId = 1,
                DoctorId = 1
            });

            _prescriptions.Add(new Prescription
            {
                Id = 2,
                Text = "Leg pain prescription.",
                PatientId = 1,
                DoctorId = 1
            });

            return _prescriptions;
        }

        private List<PrescriptionDto> CreatePrescriptionDTOs()
        {
            _prescriptions.ForEach(prescription => _prescriptionDTOs.Add(PrescriptionAdapter.PrescriptionToPrescriptionDto(prescription)));

            return _prescriptionDTOs;
        }

        private PrescriptionDto CreatePrescriptionDto()
        {
            return new PrescriptionDto
            {
                Id = 1,
                Text = "Migrenes prescription.",
                PatientId = 1,
                DoctorId = 1
            };
        }

        private Prescription CreatePrescription()
        {
            return new Prescription
            {
                Id = 1,
                Text = "Migrenes prescription.",
                PatientId = 1,
                DoctorId = 1
            };
        }
        #endregion HelperFunctions
    }
}
