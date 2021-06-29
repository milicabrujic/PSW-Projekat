using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IUserService
    {
        public UserDto Login(LoginDto loginDto);

        public User CheckIfUsernameIsValid(string Username);

        public bool CheckIfPasswordIsValid(string userPassword, string loginDtoPassword);
        
        public string AddClaims(User user);

    }
}
