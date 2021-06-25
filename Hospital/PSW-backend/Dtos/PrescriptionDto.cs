using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Dtos
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public List<String> DrugNames { get; set; }
    }
}
