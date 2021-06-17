using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class PatientFeedbackService : IPatientFeedbackService
    {
        private readonly IPatientFeedbackRepository _patientFeedbackRepository;

        public PatientFeedbackService(IPatientFeedbackRepository patientFeedbackRepository)
        {
            this._patientFeedbackRepository = patientFeedbackRepository;
        }

        public List<PatientFeedbackDto> GetAllPatientFeedbacks()
        {
            List<PatientFeedbackDto> patientFeedbackDTOs = new List<PatientFeedbackDto>();

            _patientFeedbackRepository.GetAll().ForEach(patientFeedback => patientFeedbackDTOs.Add(PatientFeedbackAdapter.PatientFeedbackToPatientFeedbackDto(patientFeedback)));

            return patientFeedbackDTOs;
        }
    }
}
