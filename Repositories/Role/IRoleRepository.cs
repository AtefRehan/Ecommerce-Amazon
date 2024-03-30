using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repositories.Role
{
    public interface IRoleRepository
    {
        Task ImportRoles();
        Task<bool> IsAdmin(string userId);
        Task<bool> AddAdmin(string userId);
        Task<bool> RemoveAdmin(string userId);


    }
}
