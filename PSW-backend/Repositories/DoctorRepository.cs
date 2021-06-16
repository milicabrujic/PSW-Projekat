using PSW_backend.Models;
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

        public List<Doctor> GetGeneralDoctors()
        {
            return _applicationDbContext.Doctors.Where(doctor => doctor.Type.Equals(Enums.DoctorType.General)).ToList();
        }
    }
}
