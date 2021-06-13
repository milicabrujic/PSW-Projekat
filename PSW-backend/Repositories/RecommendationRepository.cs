using System;
using PSW_backend.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSW_backend.Models;

namespace PSW_backend.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RecommendationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Recommendation> GetPatientRecommendation(int patientId)
        {
            return _applicationDbContext.Recommendations.Where(recommendation => (recommendation.PatientId.Equals(patientId) && recommendation.IsDeleted == false)).ToList();
        }

        public void SaveRecommendation(Recommendation recommendation)
        {
            _applicationDbContext.Recommendations.Add(recommendation);
            _applicationDbContext.SaveChanges();
        }
    }
}
