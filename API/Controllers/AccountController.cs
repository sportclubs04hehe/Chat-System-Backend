﻿using API.Data;
using API.Model.DTOs;
using API.Model.Entities;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (await UserExist(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

            if (user == null) return BadRequest("Username or password no correct");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return BadRequest("Username or password no correct");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

            private async Task<bool> UserExist(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()); // HUY != huy
        }
    }
}
