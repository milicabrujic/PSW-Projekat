using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
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
            _recommendationService.SaveRecommendation(recommendationDto);
            return Ok(recommendationDto);
        }
    }
}
