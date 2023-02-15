using AAMCJand.Data.Base;
using AAMCJand.Data.ViewModels;
using AAMCJand.Models;
using Microsoft.EntityFrameworkCore;

namespace AAMCJand.Data.Services
{
    public class MedicinesService : EntityBaseRepository<Medicine>, IMedicinesService
    {
        private readonly AppDbContext _context;
        public MedicinesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMedicineAsync(NewMedicineVM data)
        {
            var newMovie = new Medicine()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                storeId = data.storeId,
                MenuDate = data.MenuDate,
                ExpiryDate = data.ExpiryDate,
                MediCategory = data.MediCategory,
                companyId = data.companyId
            };
            await _context.Medicines.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Actors
            foreach (var actorId in data.FormulaIds)
            {
                var newActorMovie = new Formula_Medicine()
                {
                    medicineID = newMovie.Id,
                    formulaId = actorId
                };
                await _context.Formula_Medicines.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            var mediDetails = await _context.Medicines
                .Include(c => c.Store)
                .Include(p => p.Company)
                .Include(am => am.Formula_Medicines).ThenInclude(a => a.Formula)
                .FirstOrDefaultAsync(n => n.Id == id);

            return mediDetails;
        }

        public async Task<NewMedicineDropDownsVM> GetNewMedicineDropDownVM()
        {
            var response = new NewMedicineDropDownsVM()
            {
                Formulas = await _context.Formulas.OrderBy(n => n.FullName).ToListAsync(),
                Companies = await _context.Companies.OrderBy(n => n.FullName).ToListAsync(),
                Stores = await _context.Stores.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMedicineAsync(NewMedicineVM data)
        {
            var dbMovie = await _context.Medicines.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.storeId = data.storeId;
                dbMovie.MenuDate = data.MenuDate;
                dbMovie.ExpiryDate = data.ExpiryDate;
                dbMovie.MediCategory = data.MediCategory;
                dbMovie.companyId = data.companyId;
                await _context.SaveChangesAsync();
            }
            //remove existing actor
            var existingActorDB = _context.Formula_Medicines.Where(n => n.medicineID == data.Id).ToList();
            _context.Formula_Medicines.RemoveRange(existingActorDB);
            await _context.SaveChangesAsync();

            //Add Actors
            foreach (var actorId in data.FormulaIds)
            {
                var newActorMovie = new Formula_Medicine()
                {
                    medicineID = data.Id,
                    formulaId = actorId
                };
                await _context.Formula_Medicines.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
