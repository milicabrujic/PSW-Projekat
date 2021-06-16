using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor  GetGeneralDoctorByPatientUsername(string username);
        Doctor FindById(int doctorId);
        List<Doctor> DoctorSpecialists();
        public List<Doctor> GetGeneralDoctors();
    }
}
