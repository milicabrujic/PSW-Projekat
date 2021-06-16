using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DoctorRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Doctor> DoctorSpecialists()
        {
            return _applicationDbContext.Doctors.Where(doctor => doctor.Type.Equals(Enums.DoctorType.Specialist)).ToList();
        }

        public Doctor FindById(int doctorId)
        {
            return _applicationDbContext.Doctors.FirstOrDefault(doctor => doctor.Id.Equals(doctorId));
        }

        public Doctor GetGeneralDoctorByPatientUsername(string username)
        {
            Patient patient = _applicationDbContext.Patients.FirstOrDefault(patient => patient.Username.Equals(username));
            Doctor doctor = _applicationDbContext.Doctors.FirstOrDefault(doctor => doctor.Id.Equals(patient.GeneralDoctorId));
            return doctor;
        }
    }
}
