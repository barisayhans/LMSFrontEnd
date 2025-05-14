using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using System;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "basicuser",
                Email = "basicuser@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Reader.ToString());
                }
            }

            // Seed 30 Additional Users
            if (userManager.Users.Count() < 31) // Ensure we don't duplicate users
            {
                var faker = new Faker<ApplicationUser>()
                    .RuleFor(u => u.UserName, f => f.Internet.UserName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.EmailConfirmed, f => true)
                    .RuleFor(u => u.PhoneNumberConfirmed, f => true)
                    .RuleFor(u => u.CreatedAt, f => DateTime.UtcNow);

                var fakeUsers = faker.Generate(30);

                foreach (var fakeUser in fakeUsers)
                {
                    if (userManager.Users.All(u => u.Email != fakeUser.Email))
                    {
                        await userManager.CreateAsync(fakeUser, "123Pa$$word!");
                        await userManager.AddToRoleAsync(fakeUser, Roles.Reader.ToString());
                    }
                }
            }
        }
    }
}
