using PSW_backend.Dtos;
using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Adapters
{
    public class DrugAdapter
    {
        public static Drug DrugDtoToDrug(DrugDto dto)
        {
            Drug drug = new Drug();
            drug.Id = dto.Id;
            drug.Name = dto.Name;
            drug.Amount = dto.Amount;
            return drug;
        }

        public static DrugDto DrugToDrugDto(Drug drug)
        {
            DrugDto dto = new DrugDto();
            dto.Id = drug.Id;
            dto.Name = drug.Name;
            dto.Amount = drug.Amount;
            return dto;
        }
    }
}
