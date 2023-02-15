using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AAMCJand.Data.Enums;
using AAMCJand.Data.Base;

namespace AAMCJand.Models
{
    public class Medicine : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime MenuDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public MediCategory MediCategory { get; set; }
        public List<Formula_Medicine> Formula_Medicines { get; set; }
        public int storeId { get; set; }
        [ForeignKey("storeId")]
        public Store Store { get; set; }

        public int companyId { get; set; }
        [ForeignKey("companyId")]
        public Company Company { get; set; }
    }
}
