using AAMCJand.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AAMCJand.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formula_Medicine>().HasKey(am => new
            {
                am.formulaId,
                am.medicineID

            });
            modelBuilder.Entity<Formula_Medicine>().HasOne(m => m.Formula).WithMany(am => am.Formula_Medicines).HasForeignKey(m => m.formulaId);
            modelBuilder.Entity<Formula_Medicine>().HasOne(m => m.Medicine).WithMany(am => am.Formula_Medicines).HasForeignKey(m => m.medicineID);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Formula_Medicine> Formula_Medicines { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
