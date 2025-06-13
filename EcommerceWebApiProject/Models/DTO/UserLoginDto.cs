using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
