using AutoMapper;
using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.Role
{
    public class RoleRepository : IRoleRepository
    {

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

        public async Task<bool> IsAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                return await userManager.IsInRoleAsync(user, "Admin");
            }
            else
            {
                logger.LogError("User not found with ID: {UserId}", userId);
                return false;
            }
        }


        public async Task<bool> AddAdmin(string userId)
        {
            try
            {
                await ImportRoles(); // Ensure roles are imported

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    logger.LogError("User not found with ID: {UserId}", userId);
                    return false;
                }

                var adminRole = await roleManager.FindByNameAsync("Admin");
                if (adminRole == null)
                {
                    logger.LogError("Admin role not found.");
                    return false;
                }

                var result = await userManager.AddToRoleAsync(user, adminRole.Name);
                if (result.Succeeded)
                {
                    logger.LogInformation("Added Admin role to user with ID: {UserId}", userId);
                    return true;
                }
                else
                {
                    logger.LogError("Failed to add Admin role to user with ID: {UserId}. Errors: {Errors}", userId, string.Join(",", result.Errors));
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add Admin role to user with ID: {UserId}", userId);
                return false;
            }
        }


        public async Task<bool> RemoveAdmin(string userId)
        {
            try
            {
                await ImportRoles(); // Ensure roles are imported

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    logger.LogError("User not found with ID: {UserId}", userId);
                    return false;
                }

                var adminRole = await roleManager.FindByNameAsync("Admin");
                if (adminRole == null)
                {
                    logger.LogError("Admin role not found.");
                    return false;
                }

                var result = await userManager.RemoveFromRoleAsync(user, adminRole.Name);
                if (result.Succeeded)
                {
                    logger.LogInformation("Removed Admin role from user with ID: {UserId}", userId);
                    return true;
                }
                else
                {
                    logger.LogError("Failed to remove Admin role from user with ID: {UserId}. Errors: {Errors}", userId, string.Join(",", result.Errors));
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove Admin role from user  with ID: {UserId}", userId);
                return false;
            }
        }



    }
}