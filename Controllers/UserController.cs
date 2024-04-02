using AutoMapper;
using ECommerce.Data;
using ECommerce.DTO.User;
using ECommerce.DTOS.User;
using ECommerce.Models;
using ECommerce.Repositories.Wish_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using ECommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration.UserSecrets;
using ECommerce.Repositories.Role;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AmazonDB context;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IRoleRepository roleRepository;
        private readonly TokenService tokenService;
        public UserController(AmazonDB _context, IConfiguration _configuration, UserManager<ApplicationUser> _userManager, IMapper _mapper, IRoleRepository _roleRepository, TokenService _tokenService)
        {
            this.configuration = _configuration;
            this.userManager = _userManager;
            this.mapper = _mapper;
            this.context= _context;
            this.roleRepository = _roleRepository;
            this.tokenService = _tokenService;
        }

        #region Register
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var NewUser = new ApplicationUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber
                };
                var creationResult = await userManager.CreateAsync(NewUser, registerDTO.Password);
                if (!creationResult.Succeeded)
                {
                    return BadRequest(creationResult.Errors);
                }
                else
                {
                    Cart cart = new Cart() { ApplicationUserId = NewUser.Id };
                    context.Carts.Add(cart);
                    context.SaveChanges();

                    NewUser.CartId = cart.CartId;
                    context.Update(NewUser);
                    context.SaveChanges();
                    try
                    {
                        await userManager.AddToRoleAsync(NewUser, "Client");
                    }
                    catch  { return Ok("Role Not Valied"); }
                    
                    return Ok(NewUser);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginDTO credentials)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user;
                try
                {
                    user = await userManager.FindByEmailAsync(credentials.Email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Unauthorized("Email Invaild," + ex);
                }

                var isLocked = await userManager.IsLockedOutAsync(user);
                if (isLocked)
                {
                    return Unauthorized("You're locked. Please try again later");
                }

                var isAuthenticated = await userManager.CheckPasswordAsync(user, credentials.Password);
                if (!isAuthenticated)
                {
                    await userManager.AccessFailedAsync(user);
                    return Unauthorized("Wrong Credentials");
                }

                var accessToken = tokenService.CreateToken(user);
                await context.SaveChangesAsync();

                return Ok(new AuthResponse
                {
                    UserId = user.Id,
                    CartId = user.CartId,
                    Email = user.Email,
                    IsAdmin = roleRepository.IsAdmin(user.Id).Result,
                    Token = accessToken,
                }); 
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region SignOut
        private readonly HashSet<string> _blacklistedTokens = new HashSet<string>();
        [HttpGet]
        [Route("LogOut")]
        public ActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _blacklistedTokens.Add(token);
            return Ok("Logged out successfully");
        }

        #endregion

        #region All Users
        [HttpGet]
        [Route("Accounts")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                // Retrieve users with their orders and wishlists asynchronously
                var users = await userManager.Users
                    .Include(u => u.Orders.Where(o => !o.IsCancelled))
                    .Include(u => u.WishList.Where(w => !w.Product.IsCancelled))
                    .ToListAsync();

                // Create a list to hold user DTOs
                var userDTOs = new List<UserDTO>();

                // Loop through each user and create DTOs
                foreach (var user in users)
                {
                    // Retrieve user roles asynchronously
                    var roles = await userManager.GetRolesAsync(user);

                    // Create a new UserDTO object for the current user
                    var userDTO = new UserDTO
                    {
                        ApplicationUserId=user.Id,
                        Role = roles.ToList(), // Set user roles
                        Email = user.Email,
                        Name = user.UserName,
                        WishList_productsId = user.WishList.Select(w => w.ProductId).ToList(),
                        Orders_ID = user.Orders.Select(o => o.OrderId).ToList()
                    };

                    // Add the UserDTO to the list
                    userDTOs.Add(userDTO);
                }

                // Return the list of user DTOs
                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return StatusCode(500, "An error occurred while retrieving user data.");
            }
        }
        #endregion

    }
}
