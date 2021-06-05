using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class MedicalAppointmentAdapter
    {
        public static MedicalAppointment MedicalAppointmentDtoToMedicalsAppointment(MedicalAppointmentDto dto)
        {
            MedicalAppointment medicalAppointment = new MedicalAppointment();
            medicalAppointment.Id = dto.Id;
            medicalAppointment.Date = dto.Date;
            medicalAppointment.DoctorId = dto.DoctorId;
            medicalAppointment.PatientId = dto.PatientId;
            return medicalAppointment;
        }

        public static MedicalAppointmentDto MedicalAppointmentToMedicalAppointmentDto(MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentDto dto = new MedicalAppointmentDto();
            dto.Id = medicalAppointment.Id;
            dto.Date = medicalAppointment.Date;
            dto.PatientId = medicalAppointment.PatientId;
            dto.DoctorId = medicalAppointment.DoctorId;
            return dto;
        }
    }
}
