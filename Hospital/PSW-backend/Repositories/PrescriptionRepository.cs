using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrescriptionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void SaveNewPrescription(Prescription prescription)
        {
            _applicationDbContext.Prescriptions.Add(prescription);
            _applicationDbContext.SaveChanges();
        }
    }
}
