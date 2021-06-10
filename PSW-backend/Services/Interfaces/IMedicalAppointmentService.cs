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
        MedicalAppointment findAppointment(MedicalAppointmentDto medicalAppointmentDto, string priority);
        void saveAppointment(MedicalAppointmentDto medicalAppointmentDto);
    }
}
