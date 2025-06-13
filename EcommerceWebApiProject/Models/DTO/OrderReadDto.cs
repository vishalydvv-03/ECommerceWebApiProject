using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }

        public ICollection<OrderItemReadDto> OrderItems { get; set; }
    }
}
