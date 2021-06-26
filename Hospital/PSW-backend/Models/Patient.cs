using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    [Table("Patient")]
    public class Patient : User
    {
        public virtual ICollection<MedicalAppointment> MedicalAppointments { get; set; }
        public virtual ICollection<PatientFeedback> PatientFeedbacks { get; set; }
        public int CancelledMedicalAppointments { get; set; }
        [ForeignKey("Doctor")]
        public int GeneralDoctorId { get; set; }
        public virtual Doctor GeneralDoctor { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public DateTime LastCanceledDate { get; set; }
    }
}
