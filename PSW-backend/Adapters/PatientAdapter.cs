using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class PatientAdapter
    {
        public static Patient PatientDtoToPatient(PatientDto dto)
        {
            Patient patient = new Patient();
            patient.Id = dto.Id;
            patient.Name = dto.Name;
            patient.Surname = dto.Surname;
            patient.Username = dto.Username;
            patient.Email = dto.Email;
            patient.Password = dto.Password;
            patient.Address = dto.Address;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.Role = (Roles)Enum.Parse(typeof(Roles), dto.Role);
            patient.IsBlocked = dto.IsBlocked;
            patient.IsMalicious = dto.IsMalicious;
            patient.CancelledMedicalAppointments = dto.CancelledMedicalAppointments;
            patient.GeneralDoctorId = dto.GeneralDoctorId;
        //    patient.LastCanceledDate = new DateTime(2000, 1, 1, 0, 0, 0);
            return patient;
        }

        public static PatientDto PatientToPatientDto(Patient patient)
        {
            PatientDto dto = new PatientDto();
            dto.Id = patient.Id;
            dto.Name = patient.Name;
            dto.Surname = patient.Surname;
            dto.Username = patient.Username;
            dto.Email = patient.Email;
            dto.Password = patient.Password;
            dto.Address = patient.Address;
            dto.PhoneNumber = patient.PhoneNumber;
            dto.Role = (patient.Role.ToString());
            dto.IsBlocked = patient.IsBlocked;
            dto.IsMalicious = patient.IsMalicious;
            dto.CancelledMedicalAppointments = patient.CancelledMedicalAppointments;
            dto.GeneralDoctorId = patient.GeneralDoctorId;
            return dto;
        }
    }
}

