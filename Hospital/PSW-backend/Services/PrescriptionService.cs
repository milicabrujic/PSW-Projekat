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
        private readonly IDrugRepository _drugRepository;


        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IDrugRepository drugRepository)
        {
            this._prescriptionRepository = prescriptionRepository;
            this._drugRepository = drugRepository;

        }

        public PrescriptionDto SaveNewPrescription(PrescriptionDto prescriptionDto)
        {
            Prescription prescription = PrescriptionAdapter.PrescriptionDtoToPrescription(prescriptionDto);

            AddDrugsToPerscription(prescriptionDto, prescription);

            _prescriptionRepository.SaveNewPrescription(prescription);

            RabbitMQProducer.Send(prescriptionDto);

            return prescriptionDto;
        }

        public void AddDrugsToPerscription(PrescriptionDto prescriptionDto, Prescription prescription)
        {
            foreach (String drugName in prescriptionDto.DrugNames)
            {
                Drug drug = _drugRepository.GetDrugByName(drugName);

                if (prescription.Drugs == null)
                    prescription.Drugs = new List<Drug>();

                prescription.Drugs.Add(drug);
            }
        }
    }
}
