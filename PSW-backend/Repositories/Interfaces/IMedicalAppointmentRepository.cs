using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IMedicalAppointmentRepository
    {
        List<MedicalAppointment> GetDoctorAppointments(int doctorId);
        List<MedicalAppointment> GetPatientAppointments(int patientId);
        void SaveMedicalAppointment(MedicalAppointment medicalAppointment);
        MedicalAppointment EndMedicalAppointment(MedicalAppointment medicalAppointment);
    }
}
