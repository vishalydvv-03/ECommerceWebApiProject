using EcommerceWebApiProject.Models;

namespace EcommerceWebApiProject.Services
{
    public interface IJwtServiceToken
    {
        string GenerateToken(User user);
    }
}
