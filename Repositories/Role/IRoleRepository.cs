using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repositories.Role
{
    public interface IRoleRepository
    {
         Task ImportRoles();
         Task<IdentityResult> ToggleAdminRole(string userId);


    }
}
