using AAMCJand.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AAMCJand.Models
{
    public class Formula : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public List<Formula_Medicine> Formula_Medicines { get; set; }
    }
}
