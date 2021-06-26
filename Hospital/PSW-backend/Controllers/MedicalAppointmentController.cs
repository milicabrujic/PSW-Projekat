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
        [HttpGet("activeAppointmentsDoctor/{id?}")]
        public IActionResult GetDoctorActiveAppointments(int id)
        {
          List<MedicalAppointment> doctorActiveAppointments =  _medicalAppointmentService.GetDoctorActiveAppointments(id);
            return Ok(doctorActiveAppointments);
        }
        [HttpGet("appointmentsPatient/{id?}")]
        public IActionResult GetPatientAppointments(int id)
        {
            List<MedicalAppointmentHistoryDto> medicalAppointmentsPatient = _medicalAppointmentService.GetPatientAppointments(id);
            return Ok(medicalAppointmentsPatient);
        }
        [HttpPost("end")]
        public IActionResult EndMedicalAppointment([FromBody] MedicalAppointmentDto medicalAppointmentDto)
        {
            MedicalAppointmentDto appointment = _medicalAppointmentService.EndMedicalAppointment(medicalAppointmentDto.Id);
            return Ok(appointment);
        }
        [HttpPost("cancelAppointment/{id?}")]
        public IActionResult CancelMedicalAppointment(int id)
        {
            MedicalAppointmentDto appointmentDto = _medicalAppointmentService.CancelMedicalAppointment(id);
            return Ok(appointmentDto);
        }
    }
}
