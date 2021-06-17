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
    }
}
