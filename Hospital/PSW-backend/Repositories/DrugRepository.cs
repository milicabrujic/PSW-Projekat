using PSW_backend.Adapters;
using PSW_backend.Dtos;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DrugRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Drug> GetDrugs()
        {
            return _applicationDbContext.Drugs.ToList();
        }
        public Drug GetDrugByName(string name)
        {
            return _applicationDbContext.Drugs.FirstOrDefault(drug => drug.Name.Equals(name));
        }

        public DrugDto addDrug(Drug drug)
        {
            _applicationDbContext.Drugs.Add(drug);
            _applicationDbContext.SaveChanges();
            return DrugAdapter.DrugToDrugDto(drug);
        }
    }
}
