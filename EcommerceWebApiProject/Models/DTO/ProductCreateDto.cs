using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class ProductCreateDto
    {

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
