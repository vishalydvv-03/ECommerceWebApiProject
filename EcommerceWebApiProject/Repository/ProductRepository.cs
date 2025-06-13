using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using EcommerceWebApiProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApiProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await context.Products.Include(p => p.Category).ToListAsync();
            
            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var data = await context.Products.Include(p=>p.Category).FirstOrDefaultAsync(x=>x.ProductId==id);
            if (data == null)
            {
                return null;
            }

            return data;
        }

        public async Task<bool> AddProductAsync(ProductCreateDto dto)
        {
            var categoryExists = await context.Categories.AnyAsync(x => x.CategoryId == dto.CategoryId);

            if (categoryExists)
            {
                var productExists = await context.Products.AnyAsync(x => x.ProductName == dto.ProductName);
                if (!productExists)
                {
                    var product = new Product()
                    {
                        ProductName = dto.ProductName,
                        Price = dto.Price,
                        CategoryId = dto.CategoryId
                    };
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;   
        }


        public async Task<bool> UpdateProductAsync(ProductCreateDto dto, Product product)
        {
            if(await context.Categories.AnyAsync(x => x.CategoryId == dto.CategoryId))
            {
                product.ProductName = dto.ProductName;
                product.Price = dto.Price;
                product.CategoryId = dto.CategoryId;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
           
        }
        public async Task DeleteProductAsync(Product product)
        {
            
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

    }
}
