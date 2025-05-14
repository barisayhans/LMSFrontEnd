using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DefaultLibrarian
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultLibrarian = new ApplicationUser
            {
                UserName = "librarian",
                Email = "librarian@gmail.com",
                FirstName = "Barış",
                LastName = "Ayhan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultLibrarian.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultLibrarian.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultLibrarian, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultLibrarian, Roles.Librarian.ToString());
                }

            }
        }
    }
}
