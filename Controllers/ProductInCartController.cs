using AutoMapper;
using ECommerce.DTOS.ProductInCart;
using ECommerce.Models;
using ECommerce.Repositories.Cart_Repository;
using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.ProductInCart_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInCartController : ControllerBase
    {
        private readonly IProductInCartRepository _productInCartRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;

        public ProductInCartController(IProductInCartRepository productInCartRepo, IProductRepository productRepo, ICartRepository cartRepo, IMapper mapper)
        {
            _productInCartRepo = productInCartRepo;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
        }
        
        
        
        
        [HttpPost]
        public IActionResult Create(ProductInCartWriteDto productInCart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = _mapper.Map<ProductInCart>(productInCart);

                    // p.Id = p.Id + 5;
                    var product = _productRepo.GetById(productInCart.ProductId);
                   
                    var cart = _cartRepo.GetById(productInCart.CartId);
                    p.Product = product;
                    p.Cart = cart;
                    p.Quantity=1;

                    _productInCartRepo.Create(p);
                    _productInCartRepo.SaveChanges();
                    return Ok(_mapper.Map<ProductInCartReadDto>(p));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        
        
        
        
    }
}
