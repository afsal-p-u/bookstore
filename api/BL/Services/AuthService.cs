using BL.DTO;
using BL.Interfaces;
using BookStore.DTO;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<string> SignUp(CreateUserDTO _user)
        {
            var existingUser = await _repository.GetUserByEmail(_user.Email);

            if (existingUser != null)
            {
                return "Email already exist";
            }

            var user = new User
            {
                Email = _user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(_user.Password),
                Name = _user.Name,
                Role = _user.Role,
            };

            await _repository.AddUser(user);

            return "User registered successfully";
        } 

        public async Task<string> SignIn(LoginDTO _user)
        {
            var user = await _repository.GetUserByEmail(_user.Email);

            if (user == null)
            {
                return "Invalid email";
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(_user.Password, user.Password);

            if (!isPasswordValid)
            {
                return "Invalid password";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
