using PSW_backend.Models;
using PSW_backend.Repositories;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }
        public Doctor GetGeneralDoctor(string patientId)
        {
            return _doctorRepository.GetGeneralDoctorByPatientUsername(patientId);
        }
        public List<Doctor> GetSpecialists()
        {
            return _doctorRepository.DoctorSpecialists();
        }
    }
}
