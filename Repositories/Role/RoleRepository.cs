using AutoMapper;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repositories.Role
{
    public class RoleRepository: IRoleRepository
    {
        //private readonly AmazonDB dbContext;

        //private readonly RoleManager<IdentityRole> roleManager;
        //private readonly UserManager<ApplicationUser> userManager;
        //private readonly ILogger<RoleRepository> logger;
        ////        public RoleRepository(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager,ILogger<RoleRepository> _logger)
        ////        {
        //            this.roleManager = _roleManager;
        //            this.userManager = _userManager;
        //            this.logger = _logger;
        //        }
        //        public async void importRoles()
        //        {
        //            try
        //            {
        //                IdentityRole admin = await roleManager.FindByNameAsync("Admin");

        //                if (admin == null)
        //                {
        //                    admin = new IdentityRole("Admin");
        //                }
        //                IdentityRole client = await roleManager.FindByNameAsync("Admin");
        //                if (client == null)
        //                {
        //                    client = new IdentityRole("Client");
        //                }
        //            }
        //            catch (Exception ex) {  }
        //        }
        //        public async IdentityRole Task<AdminToggile>(string userid)
        //        {
        //            IdentityRole role;
        //            ApplicationUser user;
        //            IdentityResult result;

        //            try
        //            {
        //                importRoles();
        //            }
        //            catch (Exception ex) {

        //                logger.LogError(ex, "Roles in DataBase Already");
        //            }
        //            IdentityRole admin = await roleManager.FindByNameAsync("Admin");

        //            // Find the user
        //            try
        //            {
        //                user = await userManager.FindByIdAsync(userid);
        //                if (user == null)
        //                {

        //                }
        //                var userRoles = await userManager.GetRolesAsync(user);
        //                if(!userRoles.Contains(admin.Name))
        //                {
        //                     result = await userManager.AddToRoleAsync(user,admin.Name);
        //                }
        //                else
        //                {
        //                    result = await userManager.RemoveFromRoleAsync(user, admin.Name);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //            }


        //            await dbContext.SaveChangesAsync();
        //            return result;
        //=        }
        //    }
        //}

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RoleRepository> logger;

        public RoleRepository(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager, ILogger<RoleRepository> _logger)
        {
            this.roleManager = _roleManager;
            this.userManager = _userManager;
            this.logger = _logger;
        }

        public async Task ImportRoles()
        {
            try
            {
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (await roleManager.FindByNameAsync("Client") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Client"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to import roles.");
            }
        }

        public async Task<IdentityResult> ToggleAdminRole(string userId)
        {
            try
            {
                await ImportRoles(); // Ensure roles are imported

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    logger.LogError("User not found with ID: {UserId}", userId);
                    return null;
                }

                var adminRole = await roleManager.FindByNameAsync("Admin");
                if (adminRole == null)
                {
                    logger.LogError("Admin role not found.");
                    return null;
                }

                if (await userManager.IsInRoleAsync(user, adminRole.Name))
                {
                    return await userManager.RemoveFromRoleAsync(user, adminRole.Name);
                }
                else
                {
                    return await userManager.AddToRoleAsync(user, adminRole.Name);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to toggle admin role for user with ID: {UserId}", userId);
                return null;
            }
        }
    }
}