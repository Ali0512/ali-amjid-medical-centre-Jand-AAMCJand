using AAMCJand.Models;
using AAMCJand.Data.Enums;
using Microsoft.AspNetCore.Identity;
using AAMCJand.Data.Static;

namespace AAMCJand.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Cinema
                if (!context.Stores.Any())
                {
                    context.Stores.AddRange(new List<Store>()
                        {
                            new Store()
                            {
                                Name = "Ali amjid Store",
                                Logo = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                Description = "Ali Amjid Main Road Jand"
                            },
                            new Store()
                            {
                                Name = "junaid Pharmacy",
                                Logo = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                Description = "Main Bazar Jand"
                            },
                            new Store()
                            {
                                Name = "a & k pharmacy",
                                Logo = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                Description = "g11 Islamabad"
                            },
                            new Store()
                            {
                                Name = "Islo Pharmacy",
                                Logo = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                Description = "f8 islamabad"
                            },
                        });
                    context.SaveChanges();

                }
                //Actors
                if (!context.Formulas.Any())
                {
                    context.Formulas.AddRange(new List<Formula>()
                        {
                            new Formula()
                            {
                                FullName = "Paracetamole",
                                Description = "for pain relief medicne",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                            new Formula()
                            {
                                FullName = "atorvastatin ",
                                Description = "Cholestrol lowering medicine",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                            new Formula()
                            {
                                FullName = "accorbic",
                                Description = "vitamin type",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                            new Formula()
                            {
                                FullName = "parabolic",
                                Description = "pain back",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                        });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Companies.Any())
                {
                    context.Companies.AddRange(new List<Company>()
                        {
                            new Company()
                            {
                                FullName = "Astra Zeneca",
                                Description = "American base company",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                            new Company()
                            {
                                FullName = "Eli Lilly",
                                Description = "medicine exporter",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },
                            new Company()
                            {
                                FullName = "Pfizer",
                                Description = "microbacterial medicine hub",
                                ProfilePictureURL = "https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png"
                            },

                        });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Medicines.Any())
                {
                    context.Medicines.AddRange(new List<Medicine>()
                        {
                            new Medicine()
                            {
                                Name = "Amoxilin",
                                Description = "for better health",
                                Price = 19.50,
                                ImageURL="https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                MenuDate=DateTime.Now.AddDays(-10),
                                ExpiryDate=DateTime.Now.AddDays(-2),
                                storeId=1,
                                companyId=3,
                                MediCategory=MediCategory.Capsules
                            },
                            new Medicine()
                            {
                                Name = "Ativan",
                                Description = "for cough",
                                Price = 39.50,
                                ImageURL="https://www.freeiconspng.com/thumbs/medical-icon-png/pills-blue-icon-6.png",
                                MenuDate=DateTime.Now.AddDays(3),
                                ExpiryDate=DateTime.Now.AddDays(20),
                                storeId=1,
                                companyId=3,
                                MediCategory=MediCategory.Injections
                            },


                        });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Formula_Medicines.Any())
                {
                    context.Formula_Medicines.AddRange(new List<Formula_Medicine>()
                        {
                            new Formula_Medicine()
                            {
                                formulaId=2,
                                medicineID=2
                            },
                            new Formula_Medicine()
                            {
                                formulaId=1,
                                medicineID=2
                            },


                        });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRoleAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles Creation
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@admin.com";
                var adminUser = await userManager.FindByNameAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Id = "b74ddd14-6340-4840-95c2-db1255484325",
                        FullName = "Admin user",
                        UserName = "admin@admin.com",
                        LockoutEnabled = false,
                        PhoneNumber = "03215797745",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Ali123-Raza123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //Users
                string endUserEmail = "user@user.com";
                var endUser = await userManager.FindByNameAsync(endUserEmail);
                if (endUser == null)
                {
                    var newendUser = new ApplicationUser()
                    {
                        Id = "b74ddd12-6340-4840-95c2-db1255482347",
                        FullName = "End user",
                        UserName = "user@user.com",
                        LockoutEnabled = false,
                        Email = endUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newendUser, "Ali123-Raza123");
                    await userManager.AddToRoleAsync(newendUser, UserRoles.User);
                }
            }
        }

    }
}
