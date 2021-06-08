using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class AdministratorAdapter
    {
        public static Administrator AdministratorDtoToAdministrator(AdministratorDto dto)
        {
            Administrator administrator = new Administrator();
            administrator.Id = dto.Id;
            administrator.Name = dto.Name;
            administrator.Surname = dto.Surname;
            administrator.Username = dto.Username;
            administrator.Email = dto.Email;
            administrator.Password = dto.Password;
            administrator.Address = dto.Address;
            administrator.PhoneNumber = dto.PhoneNumber;
            administrator.Role = (Roles)Enum.Parse(typeof(Roles), dto.Role);
            administrator.IsBlocked = dto.IsBlocked;
            administrator.IsMalicious = dto.IsMalicious;
            return administrator;
        }

        public static AdministratorDto AdministratoToAdministratorDto(Administrator administrator)
        {
            AdministratorDto dto = new AdministratorDto();
            dto.Id = administrator.Id;
            dto.Name = administrator.Name;
            dto.Surname = administrator.Surname;
            dto.Username = administrator.Username;
            dto.Email = administrator.Email;
            dto.Password = administrator.Password;
            dto.Address = administrator.Address;
            dto.PhoneNumber = administrator.PhoneNumber;
            dto.Role = (administrator.Role.ToString());
            dto.IsBlocked = administrator.IsBlocked;
            dto.IsMalicious = administrator.IsMalicious;
            return dto;
        }
    }
}
