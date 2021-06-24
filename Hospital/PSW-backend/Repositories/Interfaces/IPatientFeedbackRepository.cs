using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IPatientFeedbackRepository
    {
        List<PatientFeedback> GetAll();
        void SaveChangedPatientFeedback(PatientFeedback patientFeedback);
        PatientFeedback GetPatientFeedbackById(int id);
        public void SaveNewPatientFeedback(PatientFeedback patientFeedback);

    }
}
