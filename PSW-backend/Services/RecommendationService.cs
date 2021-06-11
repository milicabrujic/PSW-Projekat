using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRepository recommendationRepository)
        {
            this._recommendationRepository = recommendationRepository;
        }
        public void SaveRecommendation(RecommendationDto recommendationDto)
        {
            Recommendation recommendation = RecommendationAdapter.RecommendatioDtoToRecommendation(recommendationDto);
            _recommendationRepository.SaveRecommendation(recommendation);
        }
    }
}
