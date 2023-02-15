using AAMCJand.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace AAMCJand.Models
{
    public class Company : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
