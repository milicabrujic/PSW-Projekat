using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class UserAdapter
    {
        public static User UserDtoToUser(UserDto dto)
        {
            User user = new User();
            user.Id = dto.Id;
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Address = dto.Address;
            user.PhoneNumber = dto.PhoneNumber;
            user.Role = dto.Role;
            user.IsBlocked = dto.IsBlocked;
            user.IsMalicious = dto.IsMalicious;
            return user;
        }

        public static UserDto UserToUserDto(User user)
        {
            UserDto dto = new UserDto();
            dto.Id = user.Id;
            dto.Name = user.Name;
            dto.Surname = user.Surname;
            dto.Username = user.Username;
            dto.Email = user.Email;
            dto.Password = user.Password;
            dto.Address = user.Address;
            dto.PhoneNumber = user.PhoneNumber;
            dto.Role = user.Role;
            dto.IsBlocked = user.IsBlocked;
            dto.IsMalicious = user.IsMalicious;
            return dto;
        }
    }
}

