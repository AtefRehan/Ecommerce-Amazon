using System.Security.Claims;
using AutoMapper;
using ECommerce.Data;
using ECommerce.DTOS.Cart;
using ECommerce.Models;
using ECommerce.Repositories.Cart_Repository;
using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.ProductInCart_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IMapper _mapper;
        private ICartRepository _cartRepository;
        private UserManager<ApplicationUser> _userManager;
        private IProductInCartRepository _productInCartRepository;
        private AmazonDB _db;
        private IProductRepository _productRepository;

        public CartController(IMapper mapper, ICartRepository cartRepository, UserManager<ApplicationUser> userManager,
            IProductInCartRepository productInCartRepository, AmazonDB db, IProductRepository productRepository)
        {
            this._mapper = mapper;
            this._cartRepository = cartRepository;
            this._userManager = userManager;
            this._productInCartRepository = productInCartRepository;
            this._db = db;
            this._productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<List<CartDto>> GetAllCarts()
        {
            // var carts = _cartRepository.GetAll();
            // return _mapper.Map<List<CartDto>>(carts);        
            var carts = _cartRepository.GetAllCarts().ToList();


            return _mapper.Map<List<CartDto>>(carts);
        }

        // [HttpGet]
        // [Route("CartByUserId")]
        // public async Task<ActionResult<CartDto>> GetCartByUserId()
        // {
        //     var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        //     var user = await _userManager.FindByEmailAsync(email);
        //
        //     var cart = _cartRepository.CartByUserId(user.Id);
        //     return _mapper.Map<CartDto>(cart);
        // }


        [HttpGet("{id}")]
        public ActionResult<CartDto> GetById(int id)
        {
            if (id == null) return BadRequest();
            Cart cart = _cartRepository.GetById(id);
            if (cart == null) return NotFound();
            return Ok(_mapper.Map<CartDto>(cart));
        }

        [HttpDelete("{id:int}", Name = "DeleteCart")]
        public IActionResult DeleteById(int id)
        {
            _cartRepository.DeleteById(id);
            return Ok();
        }


        // [HttpPost]
        // public IActionResult AddProductToCart(CartCreateDto cartDto)
        // {
        //     try
        //     {
        //         Cart newCart = new();
        //         newCart.ProductsInCart = new List<ProductInCart>();
        //         foreach (var item in cartDto.ProductIds)
        //         {
        //             newCart.ProductsInCart.Add(_productInCartRepository.GetProductInCartById(item));
        //         }
        //
        //         // _mapper.Map(cartDto, newCart);
        //         _cartRepository.SaveChanges();
        //         return Ok(_mapper.Map<CartDto>(newCart));
        //     }
        //     catch (Exception exception)
        //     {
        //         return BadRequest(exception.Message);
        //     }
        //
        //
        //     return BadRequest();
        // }


        [HttpPut("{id:int}", Name = "UpdateCart")]
        public IActionResult UpdateById(int id, [FromBody] CartUpdateDto cartDto)
        {
            if (ModelState.IsValid)
            {
                if (cartDto == null || id != cartDto.CartId)
                {
                    return BadRequest();
                }

                try
                {
                    var cartToEdit = _cartRepository.GetById(id);
                    if (cartToEdit is null)
                    {
                        return NotFound();
                    }


                    foreach (var item in cartToEdit.ProductsInCart)
                    {
                        cartToEdit.ProductsInCart.Remove(item);
                    }

                    foreach (var item in cartDto.ProductId)
                    {
                        cartToEdit.ProductsInCart.Add(_productInCartRepository.GetProductInCartById(item));
                    }


                    _mapper.Map(cartDto, cartToEdit);
                    // cartToEdit.CartId = cartDto.CartId;
                    // cartToEdit.ProductsInCart.Add(new ProductInCart()
                    // {
                    //     // CartId = cartToEdit.CartId,
                    //     // ProductId = cartToEdit.ProductsInCart.p
                    // });
                    // foreach (var productId in cartDto.ProductId)
                    // {
                    //     // Fetch the product details from the database
                    //    
                    // }

                    _cartRepository.SaveChanges();
                    return Ok(_mapper.Map<CartDto>(cartToEdit));
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.Message);
                }
            }

            return BadRequest();
        }
    }
}