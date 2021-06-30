using PSW_backend.Dtos;
using PSW_backend.Models;
using Rs.Ac.Uns.Ftn.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Services.Interfaces
{
    public interface IDrugService
    {
        List<DrugDto> GetDrugs();
        MessageResponseProto GetDrugFromPharmacy(string drugName);
    }
}
