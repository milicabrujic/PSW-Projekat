using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PatientRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Patient GetPatientByEmail(string email)
        {
            return _applicationDbContext.Patients.FirstOrDefault(patient => patient.Email.Equals(email));
        }

        public Patient GetPatientByUsername(string username)
        {
            return _applicationDbContext.Patients.FirstOrDefault(patient => patient.Username.Equals(username));
        }

        public Patient GetPatientById(int id)
        {
            return _applicationDbContext.Patients.FirstOrDefault(patient => patient.Id.Equals(id));
        }

        public List<Patient> GetAll()
        {
            return _applicationDbContext.Patients.ToList();
        }

        public void SavePatient(Patient patient)
        {
            _applicationDbContext.Patients.Add(patient);
            _applicationDbContext.SaveChanges();
        }

        public void UpdateMaliciousPatient(Patient patientFoundByUsername, bool checkDates)
        {
            if (checkDates)
            {
                patientFoundByUsername.LastCanceledDate = DateTime.Now;
                patientFoundByUsername.CancelledMedicalAppointments = 1;
                _applicationDbContext.SaveChanges();
            }
            else { 
                if(patientFoundByUsername.CancelledMedicalAppointments == 2)
                {
                    patientFoundByUsername.LastCanceledDate = DateTime.Now;
                    patientFoundByUsername.CancelledMedicalAppointments = 3;
                    patientFoundByUsername.IsMalicious = true;
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    patientFoundByUsername.LastCanceledDate = DateTime.Now;
                    patientFoundByUsername.CancelledMedicalAppointments++;
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        public void BlockPatient(string username)
        {
            Patient patient = GetPatientByUsername(username);
            patient.IsBlocked = true;
            _applicationDbContext.SaveChanges();
        }
    }
}
