using ECommerce.DTO.Role;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ECommerce.Models;
using ECommerce.Data;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly AmazonDB dbContext;

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IMapper mapper;
        public RoleController(RoleManager<IdentityRole> _roleManager,AmazonDB _dbContext, UserManager<ApplicationUser> _userManager,IMapper  _mapper)
        {
            this.dbContext = _dbContext;

            this.roleManager = _roleManager;
            this.userManager = _userManager;
            this.mapper = _mapper;

        }

        [HttpPost]
        [Route("Role")]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleDTO.Name;
                IdentityResult identityResult = await roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                    return Created();
                }
                return BadRequest(identityResult.Errors.FirstOrDefault()?.Description);
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRoleToUser(RoleUserDTO userRole)
        {
            if (userRole == null )
            {
                return BadRequest("Invalid user or role.");
            }
            IdentityRole role;
            ApplicationUser user;
            try
            {
              role = await roleManager.FindByIdAsync(userRole.roleId);

                if (role == null)
                {
                    role = await roleManager.FindByNameAsync(userRole.roleName);
                   
                }
            }
            catch (Exception ex) { return BadRequest("Role not found."); }

            if (userRole == null)
            {
                return BadRequest("Invalid user or role.");
            }
            // Find the user
            try
            {
                user = await userManager.FindByIdAsync(userRole.UserId);
                if (user == null)
                {
                    user = await userManager.FindByNameAsync(userRole.userName);
                }

            }
            catch (Exception ex) { return BadRequest("User not found."); 
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return Ok(result);
        }


        [HttpPost]
        [Route("RemoveRole")]
        public async Task<IActionResult> RemoveRoleFromUser(RoleUserDTO userRole)
        {
            if (userRole == null)
            {
                return BadRequest("Invalid user or role.");
            }
            IdentityRole role;
            ApplicationUser user;
            try
            {
                role = await roleManager.FindByIdAsync(userRole.roleId);

                if (role == null)
                {
                    role = await roleManager.FindByNameAsync(userRole.roleName);
                }
            }
            catch (Exception ex) { return BadRequest("Role not found."); }
            // Find the user
            try
            {
                user = await userManager.FindByIdAsync(userRole.UserId);
                if(user == null)
                {
                    user = await userManager.FindByNameAsync(userRole.userName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("User not found.");
            }
            var result = await userManager.RemoveFromRoleAsync(user, role.Name);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return Ok(result);
            
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetRoles()
        {
            List<IdentityRole> roles;
            try
            {
                 roles = await roleManager.Roles.ToListAsync();
            }
            catch (Exception ex) { return BadRequest("No Roles."); }

            var RolesName = roles.Select(r => mapper.Map<RoleDTO>(r)).ToList();
            return Ok(RolesName);
        }
    }

}