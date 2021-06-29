using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Authorization = PSW_backend.Models.Authorization;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        #region Variables
        private IPatientService _patientService;
        #endregion Variables

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public IActionResult RegisterPatient([FromBody] PatientDto patientDto)
        {
            if (_patientService.CheckIfPatientExists(patientDto.Username, patientDto.Email))
                return BadRequest();

            _patientService.RegisterPatient(patientDto);
            return Ok(patientDto);
        }

        [HttpPost("malicious/{username?}")]
        public IActionResult MaliciousPatient(string username)
        {
            if (!Authorization.Authorize("Administrator", Request?.Headers["Authorization"]))
                return Unauthorized();

            _patientService.CheckMaliciousPatient(username);
            return Ok();
        }

        [HttpPut("block/{username?}")]
        public IActionResult BlockPatient(string username)
        {
            if (!Authorization.Authorize("Administrator", Request?.Headers["Authorization"]))
                return Unauthorized();

            _patientService.BlockPatient(username);
            return Ok();
        }
        
        [HttpGet("maliciousPatients")]
        public IActionResult GetMaliciousPatients()
        {
            if (!Authorization.Authorize("Administrator", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<PatientDto> patientDtos = _patientService.GetMaliciousPatients();
            return Ok(patientDtos);
        }
    }
}
