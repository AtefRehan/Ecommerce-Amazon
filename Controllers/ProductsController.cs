using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Product_Repository;
// using ECommerce.Repositories.Product;
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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var item in product.GetAllProducts())
            {
                ProductDTO p = mapper.Map<ProductDTO>(item);
                products.Add(p);
            }
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDetailsDTO>> GetProductById(int id)
        {
            var target_product = product.GetById(id);
            if(target_product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDetailsDTO>(target_product));

        }



        [HttpPost]
        public async Task<ActionResult> Add(ProductCreateDTO p)
        {
            if (p == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return NotFound();
            else
            {
                var productCreated = mapper.Map<Product>(p);
                product.Create(productCreated);
                return CreatedAtAction("GetProductById", new { id = productCreated.ProductId }, p);

            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Edit(ProductUpdateDTO p, int id)
        {

            if (p == null) return BadRequest();
            if (p.productId != id) return BadRequest();
            var updated = mapper.Map<Product>(p);
            product.Update(updated);
            product.SaveChanges();
           return CreatedAtAction("GetProductById", new { id = updated.ProductId }, p);
        }






        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            product.DeleteProductById(id);
            //student s = db.students.Find(id);
            //if (s == null) return NotFound();
            //else
            //{
            //    db.students.Remove(s);
            //    db.SaveChanges();
            //    return Ok(s);
            //}

            return Ok();
        }


       
    }
}
