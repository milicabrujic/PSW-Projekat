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
        MedicalAppointment SaveMedicalAppointment(MedicalAppointment medicalAppointment);
        MedicalAppointment EndMedicalAppointment(int id);
        public List<MedicalAppointment> GetPatientMedicalAppointments(int id);
        MedicalAppointment  CancelMedicalAppointment(int id);
    }
}
