using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApiProject.Models.DTO
{
    public class UserRegisterDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        //public string Role { get; set; } 

    }
}
