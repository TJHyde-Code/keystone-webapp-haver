using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HaverGroupProject.Data
{
    public static class ApplicationDbInitializer
    {
        public static async void Initialize(IServiceProvider serviceProvider,
            bool UseMigrations = true, bool SeedSampleData = true)
        {
            #region Prepare the Database
            if (UseMigrations)
            {
                using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    try
                    {
                        //Create the database if it does not exist and apply the Migration
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.GetBaseException().Message);
                    }
                }
            }
            #endregion

            #region Seed Sample Data 
            if (SeedSampleData)
            {
                //Create Roles
                using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    try
                    {
                        string[] roleNames = { "Admin", "Sales", "Engineering", "Procurement",
                            "Production", "PIC", "ReadOnly"};

                        IdentityResult roleResult;
                        foreach (var roleName in roleNames)
                        {
                            var roleExist = await roleManager.RoleExistsAsync(roleName);
                            if (!roleExist)
                            {
                                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.GetBaseException().Message);
                    }
                }

                //Create Users
                using (var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
                {
                    try
                    {
                        string defaultPassword = "Pa55w@rd";

                        if (userManager.FindByEmailAsync("admin@hbniagara.com").Result == null)
                        {
                            IdentityUser user = new IdentityUser
                            {
                                UserName = "admin@hbniagara.com",
                                Email = "admin@hbniagara.com",
                                EmailConfirmed = true
                            };

                            IdentityResult result = userManager.CreateAsync(user, defaultPassword).Result;

                            if (result.Succeeded)
                            {
                                userManager.AddToRoleAsync(user, "Admin").Wait();
                            }
                        }
                        if (userManager.FindByEmailAsync("sales@hbniagara.com").Result == null)
                        {
                            IdentityUser user = new IdentityUser
                            {
                                UserName = "sales@hbniagara.com",
                                Email = "sales@hbniagara.com",
                                EmailConfirmed = true
                            };

                            IdentityResult result = userManager.CreateAsync(user, defaultPassword).Result;

                            if (result.Succeeded)
                            {
                                userManager.AddToRoleAsync(user, "Sales").Wait();
                            }
                        }
                        if (userManager.FindByEmailAsync("engineering@hbniagara.com").Result == null)
                        {
                            IdentityUser user = new IdentityUser
                            {
                                UserName = "engineering@hbniagara.com",
                                Email = "engineering@hbniagara.com",
                                EmailConfirmed = true
                            };

                            IdentityResult result = userManager.CreateAsync(user, defaultPassword).Result;

                            if (result.Succeeded)
                            {
                                userManager.AddToRoleAsync(user, "Engineering").Wait();
                            }
                        }
                        if (userManager.FindByEmailAsync("readonly@hbniagara.com").Result == null)
                        {
                            IdentityUser user = new IdentityUser
                            {
                                UserName = "readonly@hbniagara.com",
                                Email = "readonly@hbniagara.com",
                                EmailConfirmed = true
                            };

                            IdentityResult result = userManager.CreateAsync(user, defaultPassword).Result;
                            //Not in any role
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.GetBaseException().Message);
                    }
                }
            }
            #endregion
        }
    }
}