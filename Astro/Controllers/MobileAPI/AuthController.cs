using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Astro.Models;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var findUser = await _context.Users.FirstOrDefaultAsync(t => t.Email.Equals(model.Email));

            var user = new User()
            {
                Id = findUser.Id.ToString(),
                UserName = findUser.UserName,
                Email = findUser.Email,
                Avatar = findUser.Avatar,
                TopicsCount = findUser.TopicsCount,
                CommentsCount = findUser.CommentsCount,
                LastLoginDate = findUser.LastLoginDate,
                RegisterDate = findUser.RegisterDate
            };

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(findUser);

                user.LastLoginDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                _context.Update(findUser);
                await _context.SaveChangesAsync();

                await _signInManager.SignOutAsync();

                var token = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Encoding.ASCII.GetBytes("Ta aplikacja jest turbo fajna")) //ignore this string
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                    .AddClaim("id", user.Id.ToString())
                    .AddClaim("username", user.UserName)
                    .Encode();

                return Ok(new
                {
                    token,
                    user,
                    roles
                });
            }

            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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

                return Ok();
            }

            return BadRequest();
        }

    }
}