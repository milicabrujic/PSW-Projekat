using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class PatientFeedbackRepository : IPatientFeedbackRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PatientFeedbackRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<PatientFeedback> GetAll()
        {
            return _applicationDbContext.PatientFeedback.OrderByDescending(patientFeedback => patientFeedback.Date).ToList(); 
        }

        public void SaveChangedPatientFeedback(PatientFeedback patientFeedback)
        {
            _applicationDbContext.SaveChanges();
        }

        public PatientFeedback GetPatientFeedbackById(int id)
        {
            return _applicationDbContext.PatientFeedback.FirstOrDefault(patientFeedback => patientFeedback.Id.Equals(id));
        }
        
        public void SaveNewPatientFeedback(PatientFeedback patientFeedback)
        {
            _applicationDbContext.PatientFeedback.Add(patientFeedback);
            _applicationDbContext.SaveChanges();
        }
    }
}
