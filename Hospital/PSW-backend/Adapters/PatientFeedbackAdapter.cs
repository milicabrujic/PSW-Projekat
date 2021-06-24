using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class PatientFeedbackAdapter
    {
        public static PatientFeedback PatientFeedbackDtoToPatientFeedback(PatientFeedbackDto dto)
        {
            PatientFeedback patientFeedback = new PatientFeedback();
            patientFeedback.Id = dto.Id;
            patientFeedback.Text = dto.Text;
            patientFeedback.Date = dto.Date;
            patientFeedback.IsPosted = dto.IsPosted;
            patientFeedback.PatientId = dto.PatientId;
            return patientFeedback;
        }

        public static PatientFeedbackDto PatientFeedbackToPatientFeedbackDto(PatientFeedback patientFeedback)
        {
            PatientFeedbackDto dto = new PatientFeedbackDto();
            dto.Id = patientFeedback.Id;
            dto.Text = patientFeedback.Text;
            dto.Date = patientFeedback.Date;
            dto.IsPosted = patientFeedback.IsPosted;
            dto.PatientId = patientFeedback.PatientId;
            return dto;
        }
    }
}
