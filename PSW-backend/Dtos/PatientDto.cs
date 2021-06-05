using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Dtos
{
    public class PatientDto : UserDto
    {
        public int CancelledMedicalAppointments { get; set; }
        public int GeneralDoctorId { get; set; }
    }
}
