using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Xml;

namespace EcommerceWebApiProject.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepo;
        public CategoriesController(ICategoryRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [Authorize(Roles ="Admin,User")]
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await categoryRepo.GetAllCategoriesAsync();

            return categories;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetAllCategoryById(int id)
        {
            var category = await categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("No Category Exists");
            }
            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            var categories = await categoryRepo.GetAllCategoriesAsync();
            var data = categories.FirstOrDefault(x => x.CategoryName == dto.Name);
            if (data != null)
            {
                return BadRequest("Category Exists");
            }
            var category = new Category()
            {
                CategoryName = dto.Name
            };

            await categoryRepo.AddCategoryAsync(category);

            return Ok("Category Added Successfully");
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            var category = await categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("No Category Exists");
            }

            category.CategoryName = dto.Name;

            await categoryRepo.UpdateCategoryAsync();

            return Ok("Category Updated Succesfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("No Category Exists");
            }

            await categoryRepo.DeleteCategoryAsync(category);

            return Ok("Category Deleted Succesfully");
        }

    }
}
