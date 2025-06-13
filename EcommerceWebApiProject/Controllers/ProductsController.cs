using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApiProject.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        public ProductsController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            var data = await productRepo.GetAllProductsAsync();

            var products = data.Select(p => new ProductReadDto()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName
            }); 

            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var data = await productRepo.GetProductByIdAsync(id);

            if (data == null)
            {
                return NotFound("No Product Exists");
            }

            var product = new ProductReadDto()
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                Price = data.Price,
                CategoryId = data.CategoryId,
                CategoryName = data.Category.CategoryName
            };

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto dto)
        {
            var result = await productRepo.AddProductAsync(dto);

            if (result)
            {
                return Ok("Product Added Successfully");
            }

            return Ok("No Such Category Exists");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductCreateDto dto)
        {
            var data = await productRepo.GetProductByIdAsync(id);
            if (data == null)
            {
                return NotFound("No Such Product Exists");
            }

            var result= await productRepo.UpdateProductAsync(dto, data);

            if (result)
            {
                return Ok("Product Updated Succesfully");
            }

            return NotFound("No Such Category");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = await productRepo.GetProductByIdAsync(id);
            if (data == null)
            {
                return NotFound("No Such Product Exists");
            }

            await productRepo.DeleteProductAsync(data);
            return Ok("Data Deleted Succesfully");
            
        }
    }
}
