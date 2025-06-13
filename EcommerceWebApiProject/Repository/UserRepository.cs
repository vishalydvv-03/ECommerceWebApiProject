using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApiProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<User>>GetAllAsync()
        {
            var users = await context.Users.Where(x=>!x.IsDeleted).ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(int id )
        {
            var user = await context.Users.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDeleted = true;
            await context.SaveChangesAsync();
        }

    }
}
