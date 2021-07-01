using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private IConfiguration _config;

        public PatientService(IPatientRepository patientRepository, IConfiguration config)
        {
            this._patientRepository = patientRepository;
            this._config = config;
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
            patient.AuthenticationToken = GenerateAuthenticationToken(patient);

            _patientRepository.SavePatient(patient);
        }

        public String GenerateAuthenticationToken(User user)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimUserType = new Claim("type", user.Role.ToString());
            var claimId = new Claim("Id", user.Id.ToString());
            var claims = new List<Claim>();
            claims.Add(claimUserType);
            claims.Add(claimId);

            var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
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

