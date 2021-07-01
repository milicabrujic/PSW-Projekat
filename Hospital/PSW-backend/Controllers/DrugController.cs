using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Services.Interfaces;
using Rs.Ac.Uns.Ftn.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        #region Variables
        private IDrugService _drugService;
        #endregion Variables
        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet()]
        public IActionResult GetDrugs()
        {
            if (!Authorization.Authorize("Doctor", Request?.Headers["Authorization"]))
                return Unauthorized();

            List<DrugDto> drugs = _drugService.GetDrugs();
            return Ok(drugs);
        }
        [HttpGet("{drugName}")]
        public IActionResult GetDrug(string drugName)
        {
            if (!Authorization.Authorize("Administrator", Request?.Headers["Authorization"]))
                return Unauthorized();

            DrugDto drugDto = _drugService.GetDrugFromPharmacy(drugName);
            return Ok(drugDto);
        }
        [HttpPost()]
        public IActionResult AddDrug(DrugDto drugDto)
        {
            if (!Authorization.Authorize("Administrator", Request?.Headers["Authorization"]))
                return Unauthorized();
            DrugDto drug = _drugService.AddDrug(drugDto);
            return Ok(drug);
        }
    }
}
