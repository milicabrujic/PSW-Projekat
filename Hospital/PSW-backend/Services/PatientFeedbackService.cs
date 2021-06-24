using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
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
        private readonly IPatientRepository _patientRepository;


        public PatientFeedbackService(IPatientFeedbackRepository patientFeedbackRepository, IPatientRepository patientRepository)
        {
            this._patientFeedbackRepository = patientFeedbackRepository;
            this._patientRepository = patientRepository;
        }

        public List<PatientFeedbackDto> GetAllPatientFeedbacks()
        {
            List<PatientFeedbackDto> patientFeedbackDTOs = new List<PatientFeedbackDto>();

            _patientFeedbackRepository.GetAll().ForEach(patientFeedback => patientFeedbackDTOs.Add(PatientFeedbackAdapter.PatientFeedbackToPatientFeedbackDto(patientFeedback)));

            setUsernames(patientFeedbackDTOs);

            return patientFeedbackDTOs;
        }

        public PatientFeedbackDto ChangePatientFeedbackStatus(PatientFeedbackDto patientFeedbackDto)
        {
            PatientFeedback patientFeedback = CheckIfPatientFeedbackExists(patientFeedbackDto);

            if (patientFeedback != null)
                return ChangeStatus(patientFeedback, patientFeedbackDto);
            
            return null; 
        }

        public PatientFeedback CheckIfPatientFeedbackExists(PatientFeedbackDto patientFeedbackDto)
        {
            PatientFeedback patientFeedback = _patientFeedbackRepository.GetPatientFeedbackById(patientFeedbackDto.Id);

            if (patientFeedback == null)
                return null;

            return patientFeedback;
        }

        public PatientFeedbackDto ChangeStatus(PatientFeedback patientFeedback, PatientFeedbackDto patientFeedbackDto)
        {
            patientFeedback.IsPosted = !patientFeedbackDto.IsPosted;
            _patientFeedbackRepository.SaveChangedPatientFeedback(patientFeedback);

            return PatientFeedbackAdapter.PatientFeedbackToPatientFeedbackDto(patientFeedback);
        }

        public PatientFeedbackDto SaveNewPatientFeedback(PatientFeedbackDto patientFeedbackDto)
        {
            PatientFeedback patientFeedback = PatientFeedbackAdapter.PatientFeedbackDtoToPatientFeedback(patientFeedbackDto);
            _patientFeedbackRepository.SaveNewPatientFeedback(patientFeedback);

            return patientFeedbackDto;
        }

        public void setUsernames(List<PatientFeedbackDto> patientFeedbackDTOs)
        {
            foreach (PatientFeedbackDto patientFeedbackDto in patientFeedbackDTOs)
            {
                patientFeedbackDto.PatientUsername = _patientRepository.GetPatientById(patientFeedbackDto.PatientId).Username;
            }
        }
    }
}
