using AAMCJand.Data.Enums;
using AAMCJand.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AAMCJand.Data.ViewModels
{
    public class NewMedicineVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime MenuDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public MediCategory MediCategory { get; set; }
        public List<int> FormulaIds { get; set; }
        public int storeId { get; set; }
        public int companyId { get; set; }
    }
}
