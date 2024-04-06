using AutoMapper;
using ECommerce.DTOS.Order;
using ECommerce.DTOS.Order.CreateOrderDTOS;
using ECommerce.DTOS.Order.ShowOrderDTOs;
using ECommerce.Models;
using ECommerce.Repositories.Order_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepo;


        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderDTO>> Delete(int id)
        {
            var order = _orderRepo.DeleteOrder(id);
            if (order != null)
            {
                return Ok(order);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public ActionResult<ICollection<OrderDTO>> GetAll()
        {
            var result = _orderRepo.GetOrders();
                return Ok(result);
        }


        [HttpGet("Cart/{cartId}")]
        public ActionResult<ICollection<OrderDTO>> GetUserOrders(int cartId)
        {
            var result = _orderRepo.GetOrdersByCartId(cartId);
            return Ok(result);
        }


        [HttpPost("user/{userId}")]
        public ActionResult<ICollection<OrderDTO>> GetUserOrders(string userId)
        {
            var result = _orderRepo.GetOrdersByUserId(userId);
            return Ok(result);
        }


        [HttpGet("{orderId:int}")]
        public ActionResult<OrderDTO> GetOrder(int orderId)
        {
            var result = _orderRepo.GetOrder(orderId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("payment/{paymentId:int}")]
        public ActionResult<ICollection<OrderDTO>> GetPaymentOrders(int paymentId)
        {
            var result = _orderRepo.GetOrdersByPaymentId(paymentId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("CreateOrder/")]
        public ActionResult<ICollection<OrderDTO>> CreateOrder(CreateOrderInputDTO inputDTO)
        {
            var result = _orderRepo.CreateOrder(cartId: inputDTO.CartId, paymentId: inputDTO.PaymentId);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
