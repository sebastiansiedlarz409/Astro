using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Astro.Controllers.MobileAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AstroDbContext _context;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AuthController(AstroDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Email.Equals(model.Email));

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                user.LastLoginDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                _context.Update(user);
                await _context.SaveChangesAsync();

                var claims = new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                //ignore string below
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ta aplikacja jest turbo fajna :)"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(12),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user
                });
            }
            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
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

                return RedirectToAction("Login", new { model = new LoginViewModel() { Email = model.Email, Password = model.Password } });
            }

            return BadRequest();
        }
    }
}