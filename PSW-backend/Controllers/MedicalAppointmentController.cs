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
    public class MedicalAppointmentController : ControllerBase
    {
        #region Variables
        private IMedicalAppointmentService _medicalAppointmentService;
        #endregion Variables
        public MedicalAppointmentController(IMedicalAppointmentService medicalAppointmentService)
        {
            _medicalAppointmentService = medicalAppointmentService;
        }

        [HttpPost("find/{priority?}")]
        public IActionResult FindMedicalAppointment([FromBody] MedicalAppointmentDto medicalAppointmentDto, string priority)
        {
            MedicalAppointmentDto appointment = _medicalAppointmentService.FindAppointment(medicalAppointmentDto, priority);
            return Ok(appointment);
        }
        [HttpPost]
        public IActionResult CreateAppointment([FromBody] MedicalAppointmentDto medicalAppointmentDto)
        {
            _medicalAppointmentService.SaveAppointment(medicalAppointmentDto);
            return Ok(medicalAppointmentDto);
        }
    }
}
