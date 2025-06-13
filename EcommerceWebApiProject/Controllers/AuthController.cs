using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApiProject.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IJwtServiceToken jwtToken;

        private readonly PasswordHasher<User> hasher = new PasswordHasher<User>();
        public AuthController(AppDbContext context, IJwtServiceToken jwtToken)
        {
            this.context = context;
            this.jwtToken = jwtToken;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var data = await context.Users.AnyAsync(u => u.Email == dto.Email && !u.IsDeleted);
            if (data)
            {
                return BadRequest("Email Id Already Exists");
            }

            var user = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hasher.HashPassword(null!, dto.Password),
                Role="User",
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
           
            return Ok("User Created Successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && !u.IsDeleted);
            if (user==null)
            {
                return Unauthorized("User Does not exist");
            }

            var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Success)
            {
                var token = jwtToken.GenerateToken(user);
                return Ok($"Successfully Logged In with token : {token}");
            }

            return Unauthorized("Invalid Email or Password");
        }
    }
}
