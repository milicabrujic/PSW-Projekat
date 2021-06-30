using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //private IConfiguration _config;

        public UserService(IUserRepository userRepository /*,IConfiguration config*/)
        {
            this._userRepository = userRepository;
            //this._config = config;
        }

        public UserDto Login(LoginDto loginDto)
        {
            User user = CheckIfUsernameIsValid(loginDto.Username);

            if (user != null)
            {
                //otkomentarisati ukoliko je potrebno prilikom logovanja admina ili doktora generisati token koji ce se rucno dodati u bazu 
                //zato sto se admin i doktor ne registruju
                //string token = AddClaims(user);
                return CheckIfPasswordIsValid(user.Password, loginDto.Password) ? UserAdapter.UserToUserDto(user) : null;
            }

            return null;
        }

        public User CheckIfUsernameIsValid(string username)
        {
            User user = _userRepository.GetUserByUsername(username);

            if (user == null)
                return null; 
        
            return user;
        }

        public bool CheckIfPasswordIsValid(string userPassword, string loginDtoPassword)
        {
            if (userPassword != loginDtoPassword)
                return false;
            
            return true;
        }

        public string AddClaims(User user)
        {
            //var issuer = _config["Jwt:Issuer"];
            //var audience = _config["Jwt:Audience"];
            //var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //var claimUserType = new Claim("type", user.Role.ToString());
            //var claimId = new Claim("Id", user.Id.ToString());
            //var claims = new List<Claim>();
            //claims.Add(claimUserType);
            //claims.Add(claimId);

            //var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var stringToken = tokenHandler.WriteToken(token);
            //return stringToken;

            return null;
        }
    }
}
