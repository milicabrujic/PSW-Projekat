using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientFeedbackController : ControllerBase
    {
        #region Variables
        private IPatientFeedbackService _patientFeedbackService;
        #endregion Variables

        public PatientFeedbackController(IPatientFeedbackService patientFeedbackService)
        {
            _patientFeedbackService = patientFeedbackService;
        }

        [HttpGet()]
        public IActionResult GetAllPatientFeedbacks()
        {
            if (_patientFeedbackService.GetAllPatientFeedbacks() == null)
                return NotFound();
            
            return Ok(_patientFeedbackService.GetAllPatientFeedbacks());
        }
    }
}
