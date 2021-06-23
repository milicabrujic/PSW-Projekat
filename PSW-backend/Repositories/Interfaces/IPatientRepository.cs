using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient GetPatientByUsername(string username);
        Patient GetPatientByEmail(string email);
        Patient GetPatientById(int id);
        List<Patient> GetAll();
        void SavePatient(Patient patient);
        void UpdateMaliciousPatient(Patient patientFoundByUsername, bool checkDates);
        void BlockPatient(string username);
    }
}
