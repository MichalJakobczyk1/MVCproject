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
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
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
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "bar123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}