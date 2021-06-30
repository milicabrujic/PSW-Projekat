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
    public class PrescriptionController : ControllerBase
    {
        #region Variables
        private IPrescriptionService _prescriptionService;
        #endregion Variables
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost()]
        public IActionResult SaveNewPrescription([FromBody] PrescriptionDto prescriptionDto)
        {
            if (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"]))
                return Unauthorized();

            _prescriptionService.SaveNewPrescription(prescriptionDto);
            return Ok(prescriptionDto);
        }
    }
}
