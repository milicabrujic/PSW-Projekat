using Newtonsoft.Json;
using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.RabbitMQ;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public bool CheckIfPatientExists(string username, string email)
        {
            Patient patientFoundByUsername = _patientRepository.GetPatientByUsername(username);
            Patient patientFoundByEmail = _patientRepository.GetPatientByEmail(email);

            if (patientFoundByUsername == null && patientFoundByEmail == null)
                return false;

            return true;
        }

        public void RegisterPatient(PatientDto patientDto)
        {
            Patient patient = PatientAdapter.PatientDtoToPatient(patientDto);
            patient.AuthenticationToken = GenerateAuthenticationToken();

            _patientRepository.SavePatient(patient);
        }

        public String GenerateAuthenticationToken()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var authenticationToken = new string(
               Enumerable.Repeat(allChar, 8)
               .Select(token => token[random.Next(token.Length)]).ToArray()).ToString();

            return authenticationToken;
        }

        public bool CompareDates(DateTime lastCancelDate, DateTime today)
        {
            //if you want to check change AddMonths to AddMinutes
            int compareDates = DateTime.Compare(lastCancelDate.AddMonths(1), today);
            if (compareDates > 0)
            {
                return false;
            }
            return true;
        }

        public PatientDto BlockPatient(string username)
        {
            _patientRepository.BlockPatient(username);
            return PatientAdapter.PatientToPatientDto(_patientRepository.BlockPatient(username));
        }

        public List<PatientDto> GetMaliciousPatients()
        {
            List<PatientDto> patientDtos = new List<PatientDto>();

            _patientRepository.GetMaliciousPatients().ForEach(maliciousPatient => patientDtos.Add(PatientAdapter.PatientToPatientDto(maliciousPatient)));

            return patientDtos;
        }

        public void CheckMaliciousPatient(string username)
        {
            Patient patientFoundByUsername = _patientRepository.GetPatientByUsername(username);
            
            DateTime lastCancelDate = patientFoundByUsername.LastCanceledDate;
            DateTime today = DateTime.Now;
           
            bool checkDates = CompareDates(patientFoundByUsername.LastCanceledDate, DateTime.Now);

            UpdateMalitiousPatient(checkDates, patientFoundByUsername);
            
            _patientRepository.UpdateMaliciousPatient();
        }

        public Patient UpdateMalitiousPatient(bool checkDates, Patient patientFoundByUsername)
        {
            if (checkDates)
            {
                patientFoundByUsername.LastCanceledDate = DateTime.Now;
                patientFoundByUsername.CancelledMedicalAppointments = 1;
            }
            else
            {
                if (patientFoundByUsername.CancelledMedicalAppointments == 2)
                {
                    patientFoundByUsername.LastCanceledDate = DateTime.Now;
                    patientFoundByUsername.CancelledMedicalAppointments = 3;
                    patientFoundByUsername.IsMalicious = true;
                }
                else
                {
                    patientFoundByUsername.LastCanceledDate = DateTime.Now;
                    patientFoundByUsername.CancelledMedicalAppointments++;
                }
            }

            return patientFoundByUsername;
        }
    }
}

