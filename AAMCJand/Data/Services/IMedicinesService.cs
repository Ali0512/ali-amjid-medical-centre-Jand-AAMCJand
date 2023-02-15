using AAMCJand.Data.Base;
using AAMCJand.Data.ViewModels;
using AAMCJand.Models;

namespace AAMCJand.Data.Services
{
    public interface IMedicinesService : IEntityBaseRepository<Medicine>
    {
        Task<Medicine> GetMedicineByIdAsync(int id);
        Task<NewMedicineDropDownsVM> GetNewMedicineDropDownVM();
        Task AddNewMedicineAsync(NewMedicineVM data);

        Task UpdateMedicineAsync(NewMedicineVM data);
    }
}
