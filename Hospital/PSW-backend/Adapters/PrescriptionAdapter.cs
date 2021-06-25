using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class PrescriptionAdapter
    {
        public static Prescription PrescriptionDtoToPrescription(PrescriptionDto dto)
        {
            Prescription prescription = new Prescription();
            prescription.Id = dto.Id;
            prescription.Text = dto.Text;
            prescription.PatientId = dto.PatientId;
            prescription.DoctorId = dto.DoctorId;
            return prescription;
        }

        public static PrescriptionDto PrescriptionToPrescriptionDto(Prescription prescription)
        {
            PrescriptionDto dto = new PrescriptionDto();
            dto.Id = prescription.Id;
            dto.Text = prescription.Text;
            dto.PatientId = prescription.PatientId;
            dto.DoctorId = prescription.DoctorId;
            return dto;
        }

    }
}
