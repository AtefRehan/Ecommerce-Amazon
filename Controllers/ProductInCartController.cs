using AutoMapper;
using ECommerce.Data;
using ECommerce.DTOS.Product;
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
        private readonly AmazonDB _db;

        public ProductInCartController(IProductInCartRepository productInCartRepo, IProductRepository productRepo,
            ICartRepository cartRepo, IMapper mapper, AmazonDB db)
        {
            _productInCartRepo = productInCartRepo;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
            _db = db;
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
                    p.Quantity = productInCart.Quantity;

                    _productInCartRepo.Create(p);
                    _productInCartRepo.SaveChanges();
                    return Ok(); //_mapper.Map<ProductInCartReadDto>(p));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpGet("{id}")]
        public ActionResult<ProductInCartReadDto> GetByCartId(int id)
        {
            var products = _productInCartRepo.GetProductsInCartByCartId(id);
            return Ok(_mapper.Map<List<ProductInCartReadDto>>(products));
        }
        //
        //     [HttpDelete("{id}")]
        //     public IActionResult Delete(int id)
        //     {
        //         if (_productInCartRepo.GetById(id) != null)
        //         {
        //             try
        //             {
        //                 var deletedproductInCart=_productInCartRepo.GetById(id);
        //                 _productInCartRepo.Delete(id);
        //                 _productInCartRepo.SaveChanges();
        //                 return NoContent();
        //
        //             }
        //             catch (Exception ex)
        //             {
        //                 return BadRequest(ex.Message);
        //             }
        //         }
        //         return NotFound();
        //     }

        [HttpDelete("Product/{id}")]
        public IActionResult DeleteByProductId(int id)
        {
            try
            {
                var deletedproductInCart = _productInCartRepo.GetProductInCartByProductId(id);
                _db.ProductInCart.Remove(deletedproductInCart);
                // _productInCartRepo.Delete(deletedproductInCart.ProductId);
                _productInCartRepo.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{productInCartId:int}/increasequantity")]
        public IActionResult IncreaseQuantity(int productInCartId)
        {
            try
            {
                var productInCart = _productInCartRepo.GetById(productInCartId);
                var product = _db.Products.Find(productInCart.ProductId);

                if (productInCart == null)
                    return NotFound();
                if (productInCart.Quantity < product.Stock)
                { productInCart.Quantity += 1; }

                _productInCartRepo.Update(productInCart);
                _productInCartRepo.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{productInCartId:int}/decreasequantity")]
        public IActionResult DecreaseQuantity(int productInCartId)
        {
            try
            {
                var productInCart = _productInCartRepo.GetById(productInCartId);

                if (productInCart == null)
                    return NotFound();

                if (productInCart.Quantity > 1)
                {
                    productInCart.Quantity -= 1;

                    _productInCartRepo.Update(productInCart);
                    _productInCartRepo.SaveChanges();

                    return Ok();
                }
                else
                {
                    _productInCartRepo.Delete(productInCart.Id);
                    _productInCartRepo.SaveChanges();

                    return Ok("Product removed from cart as quantity reached zero.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Cart/{id}")]
        public IActionResult DeleteAllByCartId(int id)
        {
            if (id == null) return NotFound();
            _productInCartRepo.DeleteProductsInCartByCartId(id);
            _productInCartRepo.SaveChanges();
            return NoContent();
        }
        [HttpGet("CartProducts/{id}")]
        public IActionResult GetProducts(int id) 
        {
            if (id == null) 
            { 
                return NotFound();
            }
            var num = _productInCartRepo.GetProductInCartCount(id);
            return Ok(num);

        }
    }
}