using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.RabbitMQ;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
       
        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            this._prescriptionRepository = prescriptionRepository;
        }

        public PrescriptionDto SaveNewPrescription(PrescriptionDto prescriptionDto)
        {
            Prescription prescription = PrescriptionAdapter.PrescriptionDtoToPrescription(prescriptionDto);
            _prescriptionRepository.SaveNewPrescription(prescription);

            RabbitMQProducer.Send(prescriptionDto);

            return prescriptionDto;
        }
    }
}
