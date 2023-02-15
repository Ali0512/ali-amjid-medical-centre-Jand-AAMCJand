using AAMCJand.Models;

namespace AAMCJand.Data.ViewModels
{
    public class NewMedicineDropDownsVM
    {
        public NewMedicineDropDownsVM()
        {
            Companies = new List<Company>();
            Stores = new List<Store>();
            Formulas = new List<Formula>();
        }
        public List<Company> Companies { get; set; }
        public List<Store> Stores { get; set; }
        public List<Formula> Formulas { get; set; }
    }
}
