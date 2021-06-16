using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Repositories;
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

        public List<DoctorDto> GetGeneralDoctors()
        {
            List<DoctorDto> doctorDTOs = new List<DoctorDto>();

            _doctorRepository.GetGeneralDoctors().ForEach(generalDoctor => doctorDTOs.Add(DoctorAdapter.DoctorToDoctorDto(generalDoctor)));

            return doctorDTOs;
        }
    }
}
