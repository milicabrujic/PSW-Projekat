using Microsoft.AspNetCore.Http;
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
    public class DoctorController : ControllerBase
    {
        #region Variables
        private IDoctorService _doctorService;
        #endregion Variables
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("patientGeneralDoctor/{patientId?}")]       // GET / patientGeneralDoctor / id
        public IActionResult GetGeneralDoctor(string patientId)
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            Doctor generalDoctor = _doctorService.GetGeneralDoctor(patientId);
            return Ok(generalDoctor);
        }

        [HttpGet("specialists")]
        public IActionResult GetSpecialists()
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]) && (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"])))
                return Unauthorized();

            List<Doctor> specialists = _doctorService.GetSpecialists();
            return Ok(specialists);
        }

        [HttpGet("generalDoctors")]
        public IActionResult GetGeneralDoctors()
        {
            if(!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<DoctorDto> generalDoctors = _doctorService.GetGeneralDoctors();
            return Ok(generalDoctors);
        }
    }
}
  