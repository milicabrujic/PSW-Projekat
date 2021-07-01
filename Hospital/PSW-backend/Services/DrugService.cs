using Grpc.Core;
using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using Rs.Ac.Uns.Ftn.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;
        private Channel channel;
        private SpringGrpcService.SpringGrpcServiceClient client;
        public DrugService(IDrugRepository drugRepository)
        {
            this._drugRepository = drugRepository;
        }

        public List<DrugDto> GetDrugs()
        {
            List<DrugDto> drugDtos = new List<DrugDto>();

            _drugRepository.GetDrugs().ForEach(drug => drugDtos.Add(DrugAdapter.DrugToDrugDto(drug)));

            return drugDtos;
        }

        public DrugDto GetDrugFromPharmacy(string drugName)
        {
            channel = new Channel("127.0.0.1:8787", ChannelCredentials.Insecure);
            client = new SpringGrpcService.SpringGrpcServiceClient(channel);
            MessageProto messageProto = new MessageProto
            {
                Message = "get drug",
                RandomInteger = 1,
                Medication = drugName
            };
            MessageResponseProto response = client.communicate(messageProto);
            DrugDto drug = FromMedicationProtoToMedication(response.Medication);
            Console.WriteLine(response.Medication);
            return drug;
           
        }

        public DrugDto FromMedicationProtoToMedication(Medication medication)
        {
            DrugDto drug = new DrugDto();
            drug.Id = (int)medication.Id;
            drug.Name = medication.Name;
            drug.Amount = (int)medication.Amount;
            return drug;
        }

        public DrugDto AddDrug(DrugDto drugDto)
        {
            Drug drug = DrugAdapter.DrugDtoToDrug(drugDto); 
            return _drugRepository.addDrug(drug);
        }
    }
}
