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
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RoleController> logger;
        public RoleController(RoleManager<IdentityRole> _roleManager, IMapper _mapper, IRoleRepository _roleRepository, ILogger<RoleController> _logger)
        {
            this.roleRepository = _roleRepository;
            this.roleManager = _roleManager;
            this.mapper = _mapper;
            this.logger = _logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            try
            {
                var roles = await roleManager.Roles.ToListAsync();
                var roleDTOs = mapper.Map<IEnumerable<RoleDTO>>(roles);
                return Ok(roleDTOs);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get roles.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get roles.");
            }
        }

        [HttpPost("IsAdmin")]
        public async Task<ActionResult<bool>> IsAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var isAdmin = await roleRepository.IsAdmin(userId);
                return Ok(isAdmin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to check if user with ID {UserId} is an admin.", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to check if user is an admin.");
            }
        }

        [HttpPost("AddAdmin")]
        public async Task<ActionResult<bool>> AddAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var isAdmin = await roleRepository.AddAdmin(userId);
                return Ok(isAdmin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add admin role for user with ID {UserId}.", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add admin role.");
            }
        }

        [HttpPost("RemoveAdmin")]
        public async Task<ActionResult<bool>> RemoveAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var isAdmin = await roleRepository.RemoveAdmin(userId);
                return Ok(isAdmin);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove admin role for user with ID {UserId}.", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove admin role.");
            }
        }
    }
}