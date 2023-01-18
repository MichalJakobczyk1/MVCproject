using Microsoft.AspNetCore.Identity;
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

                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee()
                        {
                           HiredOn = new DateTime(2018,02,12),
                           Name = "Steve",
                           Surname = "Jones",
                           DateOfBirth = new DateTime(1995,12,24),
                           ContactNumber = "567823442",
                           Email = "Jones.business@gmail.com"
                         },
                         new Employee()
                        {
                           HiredOn = new DateTime(2019,05,22),
                           Name = "John",
                           Surname = "Smith",
                           DateOfBirth = new DateTime(1997,05,23),
                           ContactNumber = "881723909",
                           Email = "SmithJ123@gmail.com"
                         }
                    });
                    context.SaveChanges();
                }
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(new List<Order>()
                    {
                        new Order()
                        {
                           Supplier = "Makro",
                           PriceTotal = 350,
                           IsPaid = true,
                        },
                        new Order()
                        {
                           Supplier = "Makro",
                           PriceTotal = 500,
                           IsPaid = true,
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

                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Employee))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                string managerUserEmail = "Jones.business@gmail.com";

                var managerUser = await userManager.FindByEmailAsync(managerUserEmail);
                if (managerUser == null)
                {
                    var newManagerUser = new Employee()
                    {
                        UserName = "JonesSteve",
                        Email = managerUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newManagerUser, "Manager123");
                    await userManager.AddToRoleAsync(newManagerUser, UserRoles.Manager);
                }

                string appUserEmail = "SmithJ123@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new Employee()
                    {
                        UserName = "Smith",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Employee);
                }
            }
        }
    }
}
