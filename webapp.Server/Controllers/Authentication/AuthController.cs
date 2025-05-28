using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using webapp.Infrastrcture;
using Domain.Entities;
using webapp.SharedServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly IConfiguration _config;

        public AuthController(DatabaseContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                return BadRequest("Username taken");

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DisplayName = $@"{request.FirstName} {request.LastName}",
                PasswordHash = request.Password.GeneratePasswordHash(),
            };
            var roles = await _dbContext.Roles.Where(r => request.Roles.Contains(r.Name)).ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { User = user, Role = role });
            }
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _dbContext.Users.Include(x=>x.UserRoles).ThenInclude(x=>x.Role).FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized();

            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, string.Join(',',user.UserRoles.Select(x=>x.Role.Name))),
                new Claim("displayName",user.DisplayName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
               
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var _token = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse(token: _token));
        }

    }
}
