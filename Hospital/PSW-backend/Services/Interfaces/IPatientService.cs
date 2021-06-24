using PSW_backend.Dtos;
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
    }
}
