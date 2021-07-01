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
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            MedicalAppointmentDto appointment = _medicalAppointmentService.FindAppointment(medicalAppointmentDto, priority);
            return Ok(appointment);
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] MedicalAppointmentDto medicalAppointmentDto)
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            _medicalAppointmentService.SaveAppointment(medicalAppointmentDto);
            return Ok(medicalAppointmentDto);
        }

        [HttpGet("activeAppointmentsDoctor/{id?}")]
        public IActionResult GetDoctorActiveAppointments(int id)
        {
            if (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<MedicalAppointment> doctorActiveAppointments =  _medicalAppointmentService.GetDoctorActiveAppointments(id);
            return Ok(doctorActiveAppointments);
        }

        [HttpGet("appointmentsPatient/{id?}")]
        public IActionResult GetPatientAppointments(int id)
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<MedicalAppointmentHistoryDto> medicalAppointmentsPatient = _medicalAppointmentService.GetPatientAppointments(id);
            return Ok(medicalAppointmentsPatient);
        }

        [HttpPost("end")]
        public IActionResult EndMedicalAppointment([FromBody] MedicalAppointmentDto medicalAppointmentDto)
        {
            if (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"]))
                return Unauthorized();

            MedicalAppointmentDto appointment = _medicalAppointmentService.EndMedicalAppointment(medicalAppointmentDto.Id);
            return Ok(appointment);
        }

        [HttpPost("cancelAppointment/{id?}")]
        public IActionResult CancelMedicalAppointment(int id)
        {
            if (!Authorization.Authorize("Patient", Request?.Headers["Authorization"]))
                return Unauthorized();

            MedicalAppointmentDto appointmentDto = _medicalAppointmentService.CancelMedicalAppointment(id);
            return Ok(appointmentDto);
        }
    }
}
