using ECommerce.DTO.Role;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Data;
using ECommerce.Repositories.Role;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> _roleManager,  IMapper _mapper, IRoleRepository _roleRepository)
        {
            this.roleRepository = _roleRepository;
            this.roleManager = _roleManager;
            this.mapper = _mapper;
        }


        [HttpPost("UserRole/{userId}")]
        public async Task<IActionResult> ToggleAdminRole(string userId)
        {
            try
            {
                var result = await roleRepository.ToggleAdminRole(userId);
                if (result.Succeeded)
                {
                    return Ok("Role updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update role.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    
        [HttpGet]
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