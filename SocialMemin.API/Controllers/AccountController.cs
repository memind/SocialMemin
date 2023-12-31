﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMemin.API.DTOs;
using SocialMemin.API.Services;
using SocialMemin.Domain;
using System.Security.Claims;

namespace SocialMemin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _manager;
        private readonly TokenService _token;
        public AccountController(UserManager<AppUser> userManager, TokenService token)
        {
            _manager = userManager;
            _token = token;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _manager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _manager.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Email == login.Email);

            if (user == null) 
                return Unauthorized();

            var result = await _manager.CheckPasswordAsync(user, login.Password);

            if (result)
                return CreateUserObject(user);

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await _manager.Users.AnyAsync(u => u.UserName == register.Username))
            {
                ModelState.AddModelError("username", "Username is already taken");
                return ValidationProblem();
            }

            if (await _manager.Users.AnyAsync(u => u.Email == register.Email))
            {
                ModelState.AddModelError("email", "Email is already taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                UserName = register.Username
            };

            var result = await _manager.CreateAsync(user, register.Password);

            if (result.Succeeded)
                return CreateUserObject(user);

            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpDelete]
        public async Task DeleteAll()
        {
            var users = _manager.Users;

            foreach (var user in users)
                await _manager.DeleteAsync(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = user?.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                Token = _token.CreateToken(user),
                Username = user.UserName
            };
        }
    }
}
