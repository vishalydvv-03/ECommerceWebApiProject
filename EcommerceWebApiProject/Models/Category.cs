using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
       
    }   
}
