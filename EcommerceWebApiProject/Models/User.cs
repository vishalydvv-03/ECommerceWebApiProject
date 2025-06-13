using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        public bool IsDeleted { get; set; } = false;

        public ICollection<Order> Users { get; set; } = new List<Order>();

    }
}
