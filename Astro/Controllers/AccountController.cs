using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

                if (user is null)
                    return RedirectToAction("MainPage", "Forum");

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

        [Authorize]
        public IActionResult UserPage()
        {
            ViewBag.password = TempData["password"];
            ViewBag.avatar = TempData["image"];

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserPage(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userManager
                .ChangePasswordAsync(await _userManager.GetUserAsync(User), model.Password, model.NewPassword);

            TempData["password"] = "Zmieniono hasło!";

            return RedirectToAction("UserPage");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAvatar(IFormFile image)
        {
            if (image is { })
            {
                if (image.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(image.ContentDisposition).FileName.Trim('"');

                    var avatarName = image.FileName.Split('.')[0] + '_' + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") +
                        '.' + image.FileName.Split('.')[1];

                    using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\Avatars", avatarName), FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    User user = await _context.Users.FirstOrDefaultAsync(t => t.Id == _userManager.GetUserId(User));

                    if(user.Avatar is { })
                    {
                        var avatar = new System.IO.FileInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\Avatars\\" + user.Avatar);
                        avatar.Delete();
                    }

                    user.Avatar = avatarName;

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }

                TempData["image"] = "Dodano awatar!";

                return RedirectToAction("UserPage");
            }
            else
            {
                TempData["image"] = "Dodawanie awatara nie powiodło się!";
            }
            return RedirectToAction("UserPage");
        }
    }
}
