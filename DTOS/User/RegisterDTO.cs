using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.DTOS.User
{
    public record RegisterDTO
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string PhoneNumber { get; init; }
    }
}
