using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApiProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IUserRepository userRepo;
        public UsersController(AppDbContext context, IUserRepository userRepo)
        {
            this.context = context;
            this.userRepo = userRepo;
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var data = await userRepo.GetAllAsync();

            var users =  data.Select(x => new UserReadDto()
            {
                Name = x.Name,
                Email = x.Email,
                Role = x.Role,

            }).ToList();

            return Ok(users);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetById(int id)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("No User exist with this id");
            }

            var dto = new UserReadDto()
            {
                Name = user.Name,
                Email = user.Email,
                Role= user.Role
            };

            return Ok(dto);
        }

        [Authorize(Roles ="User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRegisterDto dto)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("No Such User");
            }
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = new PasswordHasher<User>().HashPassword(user, dto.Password);
     
            await userRepo.UpdateAsync(user);

            return Ok("Details Updated Successfully");
        }

        [Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userRepo.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound("User Does Not Exist");
            }

            await userRepo.DeleteAsync(user);

            return Ok("User Deleted Successfully");
        }
    }
}
