using AAMCJand.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace AAMCJand.Models
{
    public class Store : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
