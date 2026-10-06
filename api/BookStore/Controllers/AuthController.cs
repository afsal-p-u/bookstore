using BL.DTO;
using BL.Interfaces;
using BookStore.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public readonly IAuthService _service;
        
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp (CreateUserDTO _user)
        {
            var result = await _service.SignUp(_user);

            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginDTO _login)
        {
            var token = await _service.SignIn(_login);

            return Ok(new { Token = token });
        }
    }
}
