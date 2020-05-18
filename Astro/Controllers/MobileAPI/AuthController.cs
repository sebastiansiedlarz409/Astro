﻿using System;
using System.Security.Claims;
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

            var user = await _context.Users.FirstOrDefaultAsync(t => t.Email.Equals(model.Email));

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                user.LastLoginDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                _context.Update(user);
                await _context.SaveChangesAsync();

                var token = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(Encoding.ASCII.GetBytes("Ta aplikacja jest turbo fajna")) //ignore this string
                    .AddClaim(ClaimTypes.Expiration, DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                    .AddClaim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    .AddClaim(ClaimTypes.Name, user.UserName)
                    .Encode();

                return Ok(new
                {
                    token,
                    user
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