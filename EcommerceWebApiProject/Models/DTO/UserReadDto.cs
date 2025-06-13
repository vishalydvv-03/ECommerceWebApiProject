using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class UserReadDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
