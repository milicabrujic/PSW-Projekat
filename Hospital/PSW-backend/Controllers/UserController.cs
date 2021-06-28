using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PSW_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Variables
        private IUserService _userService;
        private ApplicationDbContext _dbContext;
        #endregion Variables


        public UserController(IUserService userService,ApplicationDbContext applicationDbContext)
        {
            _userService = userService;
            _dbContext = applicationDbContext;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            UserDto userDto = _userService.Login(loginDto);
          //  User myUser = _dbContext.Users.FirstOrDefault(user => user.Username == userDto.Username);
            if (userDto == null)
                return NotFound();

            if (userDto.IsBlocked)
                return StatusCode(403);

          //  SetSessionStringAndClaims(myUser);
            return Ok(userDto);
        }

        public IActionResult SerializeSessionUser(User user)
        {
            JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            return Ok();
        }
        public IActionResult SetSessionString(User user)
        {
            Console.WriteLine(user.Password);
            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            }));

            return Ok();
        }

        public IActionResult SetSessionClaims(User user)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
            });
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok();
        }

        public void SetSessionStringAndClaims(User user)
        {
            UserDto userDto = UserAdapter.UserToUserDto(user);
            SetSessionClaims(user);
           // SetSessionString(user);
            
        }
    }
}
