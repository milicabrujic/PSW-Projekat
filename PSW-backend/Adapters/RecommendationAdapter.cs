
using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class RecommendationAdapter
    {
        public static Recommendation RecommendatioDtoToRecommendation(RecommendationDto dto)
        {
            Recommendation recommendation = new Recommendation();
            recommendation.Id = dto.Id;
            recommendation.PatientId = dto.PatientId;
            recommendation.SpecialistDoctorId = dto.SpecialistDoctorId;
            recommendation.IsDeleted = dto.IsDeleted;
            return recommendation;
        }

        public static RecommendationDto RecommendationToRecommendationDto(Recommendation recommendation)
        {
            RecommendationDto dto = new RecommendationDto();
            dto.Id = recommendation.Id;
            dto.PatientId = recommendation.PatientId;
            dto.SpecialistDoctorId = recommendation.SpecialistDoctorId;
            dto.IsDeleted = recommendation.IsDeleted;
            return dto;
        }
    }
}
