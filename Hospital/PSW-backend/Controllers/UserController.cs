using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Variables
        private IUserService _userService;
        private HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion Variables

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            UserDto userDto = _userService.Login(loginDto);

            if (userDto == null)
                return NotFound();

            if (userDto.IsBlocked)
                return StatusCode(403);

            return Ok(userDto);
        }

        public static implicit operator HttpClient(UserController v)
        {
            throw new NotImplementedException();
        }
    }
}
