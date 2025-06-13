using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApiProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;
        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
            
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }
        public async Task AddCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
