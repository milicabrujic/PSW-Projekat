using PSW_backend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Dtos
{
    public class MedicalAppointmentHistoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public MedicalAppointmentStatus Status { get; set; }
        public bool Cancelled { get; set; }
    }
}
