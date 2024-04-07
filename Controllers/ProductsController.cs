using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Product_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Repositories;
using ECommerce.DTOS.Product;
using AutoMapper;
using NuGet.Protocol.Core.Types;


namespace ECommerce.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository product;
        private readonly IMapper mapper;
        public ProductsController(IProductRepository _product, IMapper _mapper)
        {
            this.product = _product;
            this.mapper = _mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAll()
        {
            try 
            {
                List<ProductDTO> products = new List<ProductDTO>();
                
                foreach (var item in product.GetAllProducts())
                {
                    ProductDTO p = mapper.Map<ProductDTO>(item);
                    products.Add(p);
                }
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving products.");
            }
            

        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDetailsDTO> GetProductById(int id)
        {
            if (id == null) return BadRequest();
            var target_product = product.GetProductById(id);
            if(target_product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDetailsDTO>(target_product));

        }



        [HttpPost]
        public ActionResult Add(ProductCreateDTO p)
        {
            if (p == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                var productCreated = mapper.Map<Product>(p);
                product.Create(productCreated);
                return CreatedAtAction("GetProductById", new { id = productCreated.ProductId }, p);

            }
        }

        [HttpPut("{id}")]

        public ActionResult Edit(ProductUpdateDTO p, int id)
        {

            if (p == null) return BadRequest();
            if (p.productId != id) return BadRequest(); 
            if(!ModelState.IsValid) return BadRequest();
            var updated = mapper.Map<Product>(p);
            if(updated.IsCancelled == true) return NotFound();
            product.Update(updated);
            product.SaveChanges();
           return CreatedAtAction("GetProductById", new { id = updated.ProductId }, p);
        }






        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            product.DeleteProductById(id);
            return Ok();
        }


       
    }
}
