using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.DTO;

namespace EcommerceWebApiProject.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<bool> AddProductAsync(ProductCreateDto dto);
        Task<bool> UpdateProductAsync(ProductCreateDto dto, Product product);
        Task DeleteProductAsync(Product product);
    }
}
