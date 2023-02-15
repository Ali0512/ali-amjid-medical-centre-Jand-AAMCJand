namespace AAMCJand.Models
{
    public class Formula_Medicine
    {
        public int medicineID { get; set; }
        public Medicine Medicine { get; set; }

        public int formulaId { get; set; }
        public Formula Formula { get; set; }
    }
}
