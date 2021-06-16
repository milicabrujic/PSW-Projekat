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
using System.Linq;
using System.Text;
using Xunit;

namespace PSW_backendTest.UnitTests
{
   public class RecommendationTests
    {
        #region Variables
        private readonly Mock<IRecommendationRepository> _stubRecommendationRepository;
        private RecommendationService _recommendationService;
        private RecommendationController _recommendationController;
        private List<Recommendation> _recommendations;
        private List<RecommendationDto> _recommendationDtos;
        #endregion Variables

        public RecommendationTests()
        {

            _stubRecommendationRepository = new Mock<IRecommendationRepository>();
            _recommendations = new List<Recommendation>();
            _recommendationDtos = new List<RecommendationDto>();
        }
        #region AdapterTests
        [Fact]
        public void Adapts_recommendation_Dto_to_recommendation()
        {
            //Arange
            RecommendationDto recommendationDto = CreateRecommendationDto();

            //Act
            Recommendation recommendation = RecommendationAdapter.RecommendatioDtoToRecommendation(recommendationDto);

            //Assert
            recommendation.ShouldNotBeNull();
            recommendation.ShouldBeOfType(typeof(Recommendation));
        }

        [Fact]
        public void Adapts_recommendation_to_recommendation_Dto()
        {
            //Arange
            Recommendation recommendation = CreateRecommendation();

            //Act
            RecommendationDto recommendationDto = RecommendationAdapter.RecommendationToRecommendationDto(recommendation);

            //Assert
            recommendationDto.ShouldNotBeNull();
            recommendationDto.ShouldBeOfType(typeof(RecommendationDto));
        }
        #endregion AdapterTests
        #region helper_function
        private RecommendationDto CreateRecommendationDto()
        {
            return new RecommendationDto
            {
                Id = 1,
                PatientId = 1,
                SpecialistDoctorId = 3,
                IsDeleted = false
            };
        }
        private Recommendation CreateRecommendation()
        {
            return new Recommendation
            {
                Id = 1,
                PatientId = 1,
                SpecialistDoctorId = 3,
                IsDeleted = false
            };
        }
        private List<Recommendation> CreateRecommendations()
        {
            _recommendations.Add(new Recommendation
            {
                Id = 1,
                PatientId = 1,
                SpecialistDoctorId = 3,
                IsDeleted = false
            });

            _recommendations.Add(new Recommendation
            {
                Id = 2,
                PatientId = 1,
                SpecialistDoctorId = 3,
                IsDeleted = false
            });

            return _recommendations;
        }
        #endregion helper_function
        [Fact]
        public void save_recommendation_controller()
        {
            //Arange
            RecommendationDto recommendationDto = CreateRecommendationDto();
            _recommendationService = new RecommendationService(_stubRecommendationRepository.Object);
            _recommendationController = new RecommendationController(_recommendationService);

            //Act
            var actionResult = _recommendationController.CreateRecommendation(recommendationDto);

            //Assert
            ((actionResult as OkObjectResult).Value as RecommendationDto).ShouldBeEquivalentTo(recommendationDto);
        }
        [Fact]
        public void patient_recommendation_not_null()
        {
            //Arange
            _stubRecommendationRepository.Setup(x => x.GetPatientRecommendation(1)).Returns(CreateRecommendations().Where(Recommendation => Recommendation.PatientId == 1).ToList());
            _recommendationService = new RecommendationService(_stubRecommendationRepository.Object);
            //Act
            List<Recommendation> recommendations = _recommendationService.GetPatientRecommendation(1);

            //Assert
            recommendations.ShouldNotBeNull();
        }
    }


}
