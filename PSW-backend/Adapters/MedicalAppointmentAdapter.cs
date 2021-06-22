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

        public static MedicalAppointment MedicalAppointmentHistoryDtoToMedicalsAppointment(MedicalAppointmentHistoryDto dto)
        {
            MedicalAppointment medicalAppointment = new MedicalAppointment();
            medicalAppointment.Id = dto.Id;
            medicalAppointment.Date = dto.Date;
            medicalAppointment.DoctorId = dto.DoctorId;
            medicalAppointment.PatientId = dto.PatientId;
            medicalAppointment.Status = dto.Status;
            return medicalAppointment;
        }
        public static MedicalAppointmentHistoryDto MedicalAppointmentToMedicalAppointmentHistoryDto(MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentHistoryDto dto = new MedicalAppointmentHistoryDto();
            dto.Id = medicalAppointment.Id;
            dto.Date = medicalAppointment.Date;
            dto.PatientId = medicalAppointment.PatientId;
            dto.DoctorId = medicalAppointment.DoctorId;
            dto.Status = medicalAppointment.Status;
            DateTime today = DateTime.Now;
            int compareDates = DateTime.Compare(today.AddDays(2), medicalAppointment.Date);
            if (compareDates < 0)
            {
                dto.Cancelled = true;
            }
            else
            {
                dto.Cancelled = false;
            }
            return dto;
        }
    }
}
