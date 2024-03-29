using ECommerce.DTO.User;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ECommerce.DTO.Role
{
    public record RoleUserDTO
    {
        public string? userName { get; set; }
        public String? roleName { get; set; }
        public String? UserId { get; set; }
        public String? roleId { get; set; }
        

    }
}
