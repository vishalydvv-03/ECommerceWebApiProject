using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.DTO;
using EcommerceWebApiProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepo;
        public OrdersController(IOrderRepository orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrders()
        {
            var data = await orderRepo.GetAllOrdersAsync();

            var orders = data.Select(x => new OrderReadDto()
            {
                OrderId = x.OrderId,
                OrderDate = x.OrderDate,
                UserId = x.UserId,
                UserName = x.User.Name,
                UserEmail = x.User.Email,
                OrderItems = x.OrderItems.Select(oi => new OrderItemReadDto()
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.ProductName,
                    Price = oi.Product.Price
                }).ToList()
            });

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(int id)
        {
            var data = await orderRepo.GetOrderByIdAsync(id);
            if (data == null)
            {
                return NotFound("No Order Exists");
            }

            var order = new OrderReadDto()
            {
                OrderId = data.OrderId,
                OrderDate = data.OrderDate,
                UserId = data.UserId,
                UserName = data.User.Name,
                UserEmail = data.User.Email,
                OrderItems = data.OrderItems.Select(oi => new OrderItemReadDto()
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.ProductName,
                    Price = oi.Product.Price
                }).ToList()
            };

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrders(OrderCreateDto dto)
        {
            var result = await orderRepo.AddOrderAsync(dto);
            if (result == false)
            {
                return NotFound("User or Product Does Not Exist");
            }
            return Ok("Order Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrders(int id, OrderCreateDto dto)
        {
            var result = await orderRepo.UpdateOrderAsync(id, dto);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var result = await orderRepo.DeleteOrderAsync(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok("Deleted Successfully");
        }
    }
}
