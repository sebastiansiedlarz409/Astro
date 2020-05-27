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

            if(roleManager.FindByNameAsync("Administrator").Result is null)
                roleManager.CreateAsync(adminRole).Wait();
            if (roleManager.FindByNameAsync("Administrator").Result is null)
                roleManager.CreateAsync(userRole).Wait();

            User admin = new User()
            {
                UserName = "admin@astro.pl",
                Email = "admin@astro.pl",
                NormalizedEmail = "ADMIN@ASTRO.PL",
                EmailConfirmed = true,
                RegisterDate = DateTime.Now.ToString()
            };

            if (userManager.FindByNameAsync("admin@astro.pl").Result is null)
            {
                IdentityResult result = userManager.CreateAsync(admin, "Zaq12345").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Administrator").Wait();
                }
            }
        }
    }
}
