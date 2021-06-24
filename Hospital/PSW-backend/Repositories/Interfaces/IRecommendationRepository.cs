using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IRecommendationRepository
    {
        void SaveRecommendation(Recommendation recommendation);
        List<Recommendation> GetPatientRecommendation(int patientId);
    }
}
