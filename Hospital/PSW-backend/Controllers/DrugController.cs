using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
using PSW_backend.Services.Interfaces;
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
            List<DrugDto> drugs = _drugService.GetDrugs();
            return Ok(drugs);
        }
    }
}
