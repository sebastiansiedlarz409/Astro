using Astro.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Astro.DAL.Configuration
{
    public class SeedAdmin
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityRole adminRole = new IdentityRole()
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };
            IdentityRole userRole = new IdentityRole()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            roleManager.CreateAsync(adminRole).Wait();
            roleManager.CreateAsync(userRole).Wait();

            User admin = new User()
            {
                UserName = "admin@astro.pl",
                Email = "admin@astro.pl",
                NormalizedEmail = "ADMIN@ASTRO.PL",
                EmailConfirmed = true,
                RegisterDate = DateTime.Now.ToString()
            };

            IdentityResult result = userManager.CreateAsync(admin, "Zaq12345").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "Administrator").Wait();
            }
        }
    }
}
