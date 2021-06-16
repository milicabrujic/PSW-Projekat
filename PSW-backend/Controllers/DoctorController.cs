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

        [HttpGet("generalDoctors")]
        public IActionResult GetGeneralDoctors()
        {
            List<DoctorDto> generalDoctors = _doctorService.GetGeneralDoctors();
            return Ok(generalDoctors);
        }
    }
}
