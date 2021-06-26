using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MedicalAppointmentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public MedicalAppointment CancelMedicalAppointment(int id)
        {
            MedicalAppointment foundedAppointment = _applicationDbContext.MedicalAppointments.Find(id);
            foundedAppointment.Status = MedicalAppointmentStatus.Cancelled;
            _applicationDbContext.SaveChanges();
            return foundedAppointment;
        }

        public MedicalAppointment EndMedicalAppointment(int id)
        {
            MedicalAppointment foundedAppointment = _applicationDbContext.MedicalAppointments.Find(id);
            foundedAppointment.Status = MedicalAppointmentStatus.Done;
            _applicationDbContext.SaveChanges();
            return foundedAppointment;
        }

        public List<MedicalAppointment> GetDoctorAppointments(int doctorId)
        {
            return _applicationDbContext.MedicalAppointments.Where(appointment => (appointment.DoctorId.Equals(doctorId) && appointment.Status.Equals(MedicalAppointmentStatus.Active))).ToList();
        }

        public List<MedicalAppointment> GetPatientAppointments(int patientId)
        {
            return _applicationDbContext.MedicalAppointments.Where(appointment => appointment.PatientId.Equals(patientId)).ToList();
        }

        public List<MedicalAppointment> GetPatientMedicalAppointments(int id)
        {
            return _applicationDbContext.MedicalAppointments.Where(appointment => appointment.PatientId.Equals(id)).ToList();
        }

        public MedicalAppointment SaveMedicalAppointment(MedicalAppointment medicalAppointment)
        {
            _applicationDbContext.MedicalAppointments.Add(medicalAppointment);
            _applicationDbContext.SaveChanges();
            return medicalAppointment;
        }
    }
}
