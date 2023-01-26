using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using MVCproject.Data;
using MVCproject.Models;
using System.Diagnostics;
using System.Net;

namespace MVCproject.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.RegularCustomers.Any())
                {
                    context.RegularCustomers.AddRange(new List<RegularCustomer>()
                    {
                        new RegularCustomer()
                        {
                            Name = "John",
                            Surname = "Wick",
                            SinceWhen = new DateTime(12,12,2008,0,0,0),
                            Nip = "67893412"
                         },
                        new RegularCustomer()
                        {
                            Name = "Jeff",
                            Surname = "Brown",
                            SinceWhen = new DateTime(8,6,2010,0,0,0),
                            Nip = "78931456"
                         },
                    });
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                string adminUserEmail = "michaljakobczyk@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Employee()
                    {
                        UserName = "michaljakobczyk",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "bar1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}