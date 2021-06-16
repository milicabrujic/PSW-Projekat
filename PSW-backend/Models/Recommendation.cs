using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    public class Recommendation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        [ForeignKey("Doctor")]
        public int SpecialistDoctorId { get; set; }
        public virtual Doctor SpecialistDoctor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
