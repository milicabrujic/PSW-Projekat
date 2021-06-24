using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IMedicalAppointmentService
    {
        MedicalAppointmentDto FindAppointment(MedicalAppointmentDto medicalAppointmentDto, string priority);
        void SaveAppointment(MedicalAppointmentDto medicalAppointmentDto);
        DateTime FindDoctorTime(Doctor doctor, DateTime dateTime);
        Doctor FindDoctorSpecialist(Doctor specialist, DateTime dateTime);
        Doctor ChangeDoctor(DateTime dateTime);
        bool CheckDateEquality(Doctor doctor, DateTime dateTime);
        List<MedicalAppointment> GetDoctorActiveAppointments(int id);
        MedicalAppointment EndMedicalAppointment(MedicalAppointmentDto medicalAppointmentDto);
        List<MedicalAppointmentHistoryDto> GetPatientAppointments(int id);
        MedicalAppointmentDto CancelMedicalAppointment(int id);
    }
}
