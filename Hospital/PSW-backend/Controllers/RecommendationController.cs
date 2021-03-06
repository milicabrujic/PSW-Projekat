using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        #region Variables
        private IRecommendationService _recommendationService;
        #endregion Variables
        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpPost]
        public IActionResult CreateRecommendation([FromBody] RecommendationDto recommendationDto)
        {
            if (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"]))
                return Unauthorized();

            _recommendationService.SaveRecommendation(recommendationDto);
            return Ok(recommendationDto);
        }

        [HttpGet("{patientId?}")]
        public IActionResult GetPatientRecommendation(int patientId)
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<Recommendation> recommendations = _recommendationService.GetPatientRecommendation(patientId);
            return Ok(recommendations);
        }
    }
}
