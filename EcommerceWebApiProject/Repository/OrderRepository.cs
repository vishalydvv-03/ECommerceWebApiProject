using EcommerceWebApiProject.Models;
using EcommerceWebApiProject.Models.Data;
using EcommerceWebApiProject.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EcommerceWebApiProject.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var data = await context.Orders.Include(x => x.User)
                .Include(x => x.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
            return data;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var data = await context.Orders.Include(x => x.User)
                .Include(x => x.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(x => x.OrderId == id);
            return data;
        }

        public async Task<bool> AddOrderAsync(OrderCreateDto dto)
        {
            var user = await context.Users.FindAsync(dto.UserId);
            if (user != null)
            {
                var productIds = dto.OrderItems.Select(x => x.ProductId).Distinct();
                var products = await context.Products.Where(x => productIds.Contains(x.ProductId)).ToListAsync();
                if (products.Count == productIds.Count())
                {
                    var order = new Order()
                    {
                        UserId = dto.UserId,
                        OrderDate = DateTime.UtcNow,
                        OrderItems = dto.OrderItems.Select(x => new OrderItem
                        {
                            ProductId = x.ProductId,
                        }).ToList()
                    };
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderCreateDto dto)
        {
            var order = await context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                if (dto.UserId == order.UserId)
                {
                    var productIds = dto.OrderItems.Select(x => x.ProductId).Distinct();
                    var products = await context.Products.Where(x => productIds.Contains(x.ProductId)).ToListAsync();
                    if (products.Count == productIds.Count())
                    {
                        context.OrderItems.RemoveRange(order.OrderItems);
                        order.OrderItems = dto.OrderItems.Select(x => new OrderItem()
                        {
                            ProductId = x.ProductId,
                            OrderId = id,
                        }).ToList();
                        order.OrderDate = DateTime.UtcNow;
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                context.OrderItems.RemoveRange(order.OrderItems);
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}           
        