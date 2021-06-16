using PSW_backend.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    [Table("Doctor")]
    public class Doctor : User
    {
        public DoctorType Type { get; set; }
        public virtual ICollection<MedicalAppointment> MedicalAppointments { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
