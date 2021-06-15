using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserDto Login(LoginDto loginDto)
        {
            User user = CheckIfUsernameIsValid(loginDto.Username);

            if (user != null)
                return CheckIfPasswordIsValid(user.Password, loginDto.Password) ? UserAdapter.UserToUserDto(user) : null;

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
    }
}
