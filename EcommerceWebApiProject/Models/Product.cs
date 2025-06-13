using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
