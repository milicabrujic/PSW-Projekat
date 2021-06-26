﻿using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IDrugRepository
    {
         List<Drug> GetDrugs();
         Drug GetDrugByName(string name);
    }
}