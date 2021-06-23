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
        [HttpGet("/malicious/{username?}")]
        public IActionResult MaliciousPatient(string username)
        {
            _patientService.CheckMaliciousPatient(username);
            return Ok();
        }
        [HttpGet("/block/{username?}")]
        public IActionResult BlockPatient(string username)
        {
            _patientService.BlockPatient(username);
            return Ok();
        }
    }
}
