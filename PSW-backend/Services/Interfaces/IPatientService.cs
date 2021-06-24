using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IPatientService
    {
        bool CheckIfPatientExists(string username, string email);
        void RegisterPatient(PatientDto patientDto);
        void CheckMaliciousPatient(string username);
        PatientDto BlockPatient(string username);
        List<PatientDto> GetMaliciousPatients();
    }
}
