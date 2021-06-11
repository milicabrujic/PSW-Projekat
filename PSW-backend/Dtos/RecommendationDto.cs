using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Dtos
{
    public class RecommendationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SpecialistDoctorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
