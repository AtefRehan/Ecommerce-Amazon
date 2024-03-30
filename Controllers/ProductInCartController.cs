using AutoMapper;
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
        
        
        
        
        
        
        
        
        
    }
}
