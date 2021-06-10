﻿using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IDoctorService
    {
        Doctor getGeneralDoctor(string patientId);
        List<Doctor> getSpecialists();
    }
}
