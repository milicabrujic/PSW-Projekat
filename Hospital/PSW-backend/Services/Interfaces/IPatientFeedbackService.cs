using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IPatientFeedbackService
    {
        public List<PatientFeedbackDto> GetAllPatientFeedbacks();
        public PatientFeedbackDto ChangePatientFeedbackStatus(PatientFeedbackDto patientFeedbackDto);
        public PatientFeedback CheckIfPatientFeedbackExists(PatientFeedbackDto patientFeedbackDto);
        public PatientFeedbackDto ChangeStatus(PatientFeedback patientFeedback, PatientFeedbackDto patientFeedbackDto);
        public PatientFeedbackDto SaveNewPatientFeedback(PatientFeedbackDto patientFeedbackDto);
        public void setUsernames(List<PatientFeedbackDto> patientFeedbackDTOs);

    }
}
