using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;

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
    }
}
