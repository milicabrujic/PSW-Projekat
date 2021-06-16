using PSW_backend.Models;
using PSW_backend.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IDoctorService
    {
        Doctor GetGeneralDoctor(string patientId);
        List<Doctor> GetSpecialists();
        List<DoctorDto> GetGeneralDoctors();
    }
}
