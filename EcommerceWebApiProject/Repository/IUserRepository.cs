using EcommerceWebApiProject.Models;

namespace EcommerceWebApiProject.Repository
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);

    }
}
