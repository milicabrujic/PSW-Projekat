using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class DoctorAdapter
    {
        public static Doctor DoctorDtoToDoctor(DoctorDto dto)
        {
            Doctor doctor = new Doctor();
            doctor.Id = dto.Id;
            doctor.Name = dto.Name;
            doctor.Surname = dto.Surname;
            doctor.Username = dto.Username;
            doctor.Email = dto.Email;
            doctor.Password = dto.Password;
            doctor.Address = dto.Address;
            doctor.PhoneNumber = dto.PhoneNumber;
            doctor.Role = (Roles)Enum.Parse(typeof(Roles), dto.Role);
            doctor.IsBlocked = dto.IsBlocked;
            doctor.IsMalicious = dto.IsMalicious;
            return doctor;
        }

        public static DoctorDto DoctorToDoctorDto(Doctor doctor)
        {
            DoctorDto dto = new DoctorDto();
            dto.Id = doctor.Id;
            dto.Name = doctor.Name;
            dto.Surname = doctor.Surname;
            dto.Username = doctor.Username;
            dto.Email = doctor.Email;
            dto.Password = doctor.Password;
            dto.Address = doctor.Address;
            dto.PhoneNumber = doctor.PhoneNumber;
            dto.Role = (doctor.Role.ToString());
            dto.IsBlocked = doctor.IsBlocked;
            dto.IsMalicious = doctor.IsMalicious;
            return dto;
        }
    }
}
