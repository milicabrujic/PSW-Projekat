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
    public class DrugTests
    {
        #region Variables
        private readonly Mock<IDrugRepository> _stubDrugRepository;
        private DrugService _drugService;
        private DrugController _drugController;
        private List<Drug> _drugs;
        private List<DrugDto> _drugDtos;
        #endregion Variables

        public DrugTests()
        {
            _stubDrugRepository = new Mock<IDrugRepository>();

            _drugs = new List<Drug>();
            _drugDtos = new List<DrugDto>();
        }

        #region AdapterTests
        [Fact]
        public void Adapts_drug_Dto_to_drug()
        {
            //Arange
            DrugDto drugDto = CreateDrugDto();

            //Act
            Drug drug = DrugAdapter.DrugDtoToDrug(drugDto);

            //Assert
            drug.ShouldNotBeNull();
            drug.ShouldBeOfType(typeof(Drug));
        }

        [Fact]
        public void Adapts_doctor_to_doctor_Dto()
        {
            //Arange
            Drug drug = CreateDrug();

            //Act
            DrugDto drugDto = DrugAdapter.DrugToDrugDto(drug);

            //Assert
            drugDto.ShouldNotBeNull();
            drugDto.ShouldBeOfType(typeof(DrugDto));
        }
        #endregion AdapterTests

        #region GetDrugsTests
        [Fact]
        public void Get_drugs()
        {
            //Arrange
            ArrangeForGetDrugs();

            //Act
            List<DrugDto> _drugDtos = _drugService.GetDrugs();

            //Assert
            _drugDtos.ShouldNotBeNull();
            _drugDtos.Count.ShouldBeEquivalentTo(2);
        }
        [Fact]
        public void Get_drugs_controller()
        {
            //Arrange
            ArrangeForGetDrugs();
         
            //Act
            var actionResult = _drugController.GetDrugs();

            //Assert
            ((actionResult as OkObjectResult).Value as List<DrugDto>).ShouldBeEquivalentTo(CreateDrugDtos());
        }
        #endregion GetDrugsTests

        #region HelperFunctios
        private DrugDto CreateDrugDto()
        {
            return new DrugDto
            {
                Id = 1,
                Name = "Aspirin",
                Amount = 5
            };
        }
        private Drug CreateDrug()
        {
            return new Drug
            {
                Id = 1,
                Name = "Aspirin",
                Amount = 5
            };
        }
        private List<Drug> CreateDrugs()
        {
            _drugs.Add(new Drug
            {
                Id = 1,
                Name = "Aspirin",
                Amount = 5
            });

            _drugs.Add(new Drug
            {
                Id = 1,
                Name = "Brufen",
                Amount = 7
            });

            return _drugs;
        }

        private List<DrugDto> CreateDrugDtos()
        {
            _drugs.ForEach(drug => _drugDtos.Add(DrugAdapter.DrugToDrugDto(drug)));

            return _drugDtos;
        }
        private void ArrangeForGetDrugs()
        {
            _stubDrugRepository.Setup(x => x.GetDrugs()).Returns(CreateDrugs());
            _drugService = new DrugService(_stubDrugRepository.Object);
            _drugController = new DrugController(_drugService);
        }
        #endregion HelperFunctions
    }
}
