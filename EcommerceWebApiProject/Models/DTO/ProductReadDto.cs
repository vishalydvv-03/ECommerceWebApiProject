using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
