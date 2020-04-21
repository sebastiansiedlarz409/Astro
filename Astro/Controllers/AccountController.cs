using System;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AstroDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AstroDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(t => t.Email.Equals(model.Email));

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    user.LastLoginDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("MainPage", "Forum");
        }

        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    RegisterDate = DateTime.Now.ToString()
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(newUser, "User").Wait();
                    
                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    return RedirectToAction("MainPage", "Forum");
                }

            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("MainPage", "Forum");
        }
    }
}