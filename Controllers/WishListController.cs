﻿using AutoMapper;
using ECommerce.DTOS.Product;
using ECommerce.Models;
using ECommerce.Repositories.Product_Repository;
using ECommerce.Repositories.Wish_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Amazon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishRepository wish;
        private readonly IMapper mapper;
        public WishListController(IWishRepository _wish, IMapper _mapper)
        {
            this.wish = _wish;
            this.mapper = _mapper;
        }
        [HttpGet]
        public IActionResult GetByUserId(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            else
            {
               var products =wish.GetWishProductsByUserId(userId);
                return Ok(products);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add(int productId, string userID)
        {
            if (productId != null && userID != null) 
            {
                wish.AddProduct(productId, userID);
                return Ok();
            }

            return BadRequest();
        }
        [HttpDelete]
        public ActionResult Delete(int productId, string userID)
        {
            if(productId != null && userID!= null)
            {
                wish.RemoveProduct(productId, userID);
                return Ok();
            }
            return BadRequest();
        }
    }
    
}