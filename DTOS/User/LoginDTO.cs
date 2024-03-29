using Microsoft.Build.Framework;

namespace ECommerce.DTO.User
{
    public class LoginDTO
    {
        //public string? Username { get; init; }
        public string Email { get; set; }
        [Required]
        public string Password { get; init; }
    }
}
