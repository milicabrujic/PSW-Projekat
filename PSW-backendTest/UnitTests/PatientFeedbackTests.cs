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
    public class PatientFeedbackTests
    {
        #region Variables
        private readonly Mock<IPatientFeedbackRepository> _stubPatientFeedbackRepository;
        private PatientFeedbackService _patientFeedbackService;
        private PatientFeedbackController _patientFeedbackController;
        private List<PatientFeedback> _patientFeedbacks;
        private List<PatientFeedbackDto> _patientFeedbackDTOs;
        #endregion Variables

        public PatientFeedbackTests()
        {
            _stubPatientFeedbackRepository = new Mock<IPatientFeedbackRepository>();

            _patientFeedbacks = new List<PatientFeedback>();
            _patientFeedbackDTOs = new List<PatientFeedbackDto>();
        }

        #region AdapterTests
        [Fact]
        public void Adapts_patient_feedback_DTO_to_patient_feedback()
        {
            //Arange
            PatientFeedbackDto patientFeedbackDTO = CreatePatientFeedbackDto();

            //Act
            PatientFeedback patientFeedback = PatientFeedbackAdapter.PatientFeedbackDtoToPatientFeedback(patientFeedbackDTO);

            //Assert
            patientFeedback.ShouldNotBeNull();
            patientFeedback.ShouldBeOfType(typeof(PatientFeedback));
        }

        [Fact]
        public void Adapts_patient_feedback_to_patient_feedback_DTO()
        {
            //Arange
            PatientFeedback patientFeedback = CreatePatientFeedback();

            //Act
            PatientFeedbackDto patientFeedbackDTO = PatientFeedbackAdapter.PatientFeedbackToPatientFeedbackDto(patientFeedback);

            //Assert
            patientFeedbackDTO.ShouldNotBeNull();
            patientFeedbackDTO.ShouldBeOfType(typeof(PatientFeedbackDto));
        }
        #endregion AdapterTests

        #region GetAllFeedbacksTests
        [Fact]
        public void Gets_all_patient_feedbacks_service()
        {
            //Arange
            ArrangeForGetAllGetAllFeedbacksTests();

            //Act
            List<PatientFeedbackDto> patientFeedbacks = _patientFeedbackService.GetAllPatientFeedbacks();

            //Assert
            _stubPatientFeedbackRepository.Verify(x => x.GetAll(), Times.Once);
            patientFeedbacks.ShouldNotBeNull();
            patientFeedbacks.ShouldNotBeEmpty();
            patientFeedbacks.Count.ShouldBeEquivalentTo(2);
        }

        [Fact]
        public void Gets_all_patient_feedbacks_controller()
        {
            //Arange
            ArrangeForGetAllGetAllFeedbacksTests();

            //Act
            var actionResult = _patientFeedbackController.GetAllPatientFeedbacks();

            //Assert
            ((actionResult as OkObjectResult).Value as List<PatientFeedbackDto>).ShouldBeEquivalentTo(_patientFeedbackDTOs);
        }
        #endregion GetAllFeedbacksTests

        #region ChangePatientFeedbackStatusTests
        [Fact]
        public void Checks_if_patient_feedback_exists_true()
        {
            //Arange
            PatientFeedback patientFeedback = CreatePatientFeedback();
            PatientFeedbackDto patientFeedbackDto = new PatientFeedbackDto {
                Id = patientFeedback.Id,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            };

            ArrangeForCheckingIfPatientFeedbackExistsTests(patientFeedbackDto);

            //Act
            PatientFeedback ValidPatientFeedback = _patientFeedbackService.CheckIfPatientFeedbackExists(patientFeedbackDto);

            //Assert
            ValidPatientFeedback.ShouldNotBeNull();
            ValidPatientFeedback.ShouldBeOfType<PatientFeedback>();
        }

        [Fact]
        public void Checks_if_patient_feedback_exists_false()
        {
            //Arange
            PatientFeedbackDto patientFeedbackDto = new PatientFeedbackDto
            {
                Id = -5,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            };

            ArrangeForCheckingIfPatientFeedbackExistsTests(patientFeedbackDto);

            //Act
            PatientFeedback ValidPatientFeedback = _patientFeedbackService.CheckIfPatientFeedbackExists(patientFeedbackDto);

            //Assert
            ValidPatientFeedback.ShouldBeNull();
            ValidPatientFeedback.ShouldNotBeOfType<PatientFeedback>();
        }

        [Fact]
        public void Checks_if_patient_status_is_changed_true()
        {
            //Arange
            PatientFeedback patientFeedback = CreatePatientFeedback();
            PatientFeedback patientFeedbackTemp = CreatePatientFeedback();
            PatientFeedbackDto patientFeedbackDto = new PatientFeedbackDto
            {
                Id = patientFeedback.Id,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = patientFeedback.IsPosted,
                PatientId = 1
            };

            ArrangeForCheckingIfStatusChangedSuccessfullyTests(patientFeedback);

            //Act
            PatientFeedbackDto changedPatientFeedback = _patientFeedbackService.ChangeStatus(patientFeedback, patientFeedbackDto);

            //Assert
            changedPatientFeedback.IsPosted.ShouldNotBe(patientFeedbackTemp.IsPosted);
        }

        [Fact]
        public void Checks_if_patient_status_is_changed_false()
        {
            //Arange
            PatientFeedback patientFeedback = CreatePatientFeedback();
            PatientFeedback patientFeedbackTemp = CreatePatientFeedback();
            PatientFeedbackDto patientFeedbackDto = new PatientFeedbackDto
            {
                Id = patientFeedback.Id,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = !patientFeedback.IsPosted,
                PatientId = 1
            };

            ArrangeForCheckingIfStatusChangedSuccessfullyTests(patientFeedback);

            //Act
            PatientFeedbackDto changedPatientFeedback = _patientFeedbackService.ChangeStatus(patientFeedback, patientFeedbackDto);

            //Assert
            changedPatientFeedback.IsPosted.ShouldBe(patientFeedbackTemp.IsPosted);
        }

        #endregion ChangePatientFeedbackStatus

        #region HelperFunctions
        private List<PatientFeedback> CreatePatientFeedbacks()
        {
            _patientFeedbacks.Add(new PatientFeedback
            {
                Id = 1,
                Text = "Great hospital.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            });

            _patientFeedbacks.Add(new PatientFeedback
            {
                Id = 2,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            });

            return _patientFeedbacks;
        }

        private List<PatientFeedbackDto> CreatePatientFeedbackDTOs()
        {
            _patientFeedbacks.ForEach(patientFeedback => _patientFeedbackDTOs.Add(PatientFeedbackAdapter.PatientFeedbackToPatientFeedbackDto(patientFeedback)));

            return _patientFeedbackDTOs;
        }

        private PatientFeedbackDto CreatePatientFeedbackDto()
        {
            return new PatientFeedbackDto
            {
                Id = 2,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            };
        }

        private PatientFeedback CreatePatientFeedback()
        {
            return new PatientFeedback
            {
                Id = 2,
                Text = "Great doctors.",
                Date = DateTime.Now,
                IsPosted = false,
                PatientId = 1
            };
        }

        private void ArrangeForGetAllGetAllFeedbacksTests()
        {
            _patientFeedbacks = CreatePatientFeedbacks();
            _patientFeedbackDTOs = CreatePatientFeedbackDTOs();
            _stubPatientFeedbackRepository.Setup(x => x.GetAll()).Returns(_patientFeedbacks);
            _patientFeedbackService = new PatientFeedbackService(_stubPatientFeedbackRepository.Object);
            _patientFeedbackController = new PatientFeedbackController(_patientFeedbackService);
        }

        private void ArrangeForCheckingIfPatientFeedbackExistsTests(PatientFeedbackDto patientFeedbackDto)
        {
            _stubPatientFeedbackRepository.Setup(x => x.GetPatientFeedbackById(patientFeedbackDto.Id)).Returns(CreatePatientFeedbacks().Find(patientFeedback => patientFeedback.Id == patientFeedbackDto.Id));
            _patientFeedbackService = new PatientFeedbackService(_stubPatientFeedbackRepository.Object);
        }
       
        private void ArrangeForCheckingIfStatusChangedSuccessfullyTests(PatientFeedback patientFeedback)
        {
            _stubPatientFeedbackRepository.Setup(x => x.SaveChangedPatientFeedback(It.IsAny<PatientFeedback>()));
            _patientFeedbackService = new PatientFeedbackService(_stubPatientFeedbackRepository.Object);
        }

        #endregion HelperFunctions
    }
}
