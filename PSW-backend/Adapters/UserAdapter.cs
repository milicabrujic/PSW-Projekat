using PSW_backend.Dtos;
using PSW_backend.Enums;
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
            user.Role = (Roles)Enum.Parse(typeof(Roles), dto.Role);
            user.IsBlocked = dto.IsBlocked;
            user.IsMalicious = dto.IsMalicious;
            user.AuthenticationToken = dto.AuthenticationToken;
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
            dto.Role = (user.Role.ToString());
            dto.IsBlocked = user.IsBlocked;
            dto.IsMalicious = user.IsMalicious;
            dto.AuthenticationToken = user.AuthenticationToken;
            return dto;
        }
    }
}

