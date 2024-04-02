using AutoMapper;
using ECommerce.Data;
using ECommerce.DTOS.Order;
using ECommerce.DTOS.Order.CreateOrderDTOS;
using ECommerce.DTOS.Order.ShowOrderDTOs;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using MailKit.Search;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Repositories.Order_Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AmazonDB _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(AmazonDB context, IMapper mapper, ILogger<OrderRepository> logger) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public OrderDTO DeleteOrder(int orderId)
        {
            try
            {
                var order = _context.Orders.Where(o => !o.IsCancelled).Include(o => o.payment)
                    .Include(o => o.OrderProducts).ThenInclude(p=>p.Product)
                    .FirstOrDefault(o => o.OrderId == orderId);
                order.IsCancelled = true;
                foreach (var item in order.OrderProducts)
                {
                    _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Stock += item.Quantity;
                    _context.SaveChanges();
                    
                }
                _context.Orders.Update(order);
                _context.SaveChanges();
                OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);
                return orderDTO;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "this Order with Id={orderId} Not Found" + orderId);
                return null;
            }

        }
        public OrderDTO GetOrder(int id)
        {
            try
            {
                var order = _context.Orders.Where(o => !o.IsCancelled).Include(p => p.payment)
                .Include(o => o.OrderProducts).ThenInclude(p=>p.Product)
                .FirstOrDefault(o => o.OrderId == id);

                if (order != null)
                {
                    return _mapper.Map<OrderDTO>(order);
                }
                _logger.LogError("Not found Order With Id={id}" + id);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed Connection With Database Not found Order With Id={id}" + id);

                return null;
            }
        }

        public ICollection<OrderDTO> GetOrders()
        {
            try
            {
                var orders = _context.Orders.Where(o => !o.IsCancelled).Include(p => p.payment)
                .Include(o => o.OrderProducts).ThenInclude(p=>p.Product)
                .ToList();

                if (orders.Count > 0)
                {
                    return _mapper.Map<List<OrderDTO>>(orders);
                }
                _logger.LogError("Not found Orders");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed Connection With Database Not found Orders");

                return null;
            }

        }

        public ICollection<OrderDTO> GetOrdersByUserId(string userId)
        {
            try
            {
                var user = _context.Users
            .Include(u => u.Orders)
            .FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    var orders = _context.Orders
                        .Include(o => o.payment)
                        .Include(o => o.OrderProducts).ThenInclude(p => p.Product)
                        .Where(o => o.ApplicationUserId == userId && !o.IsCancelled)
                        .ToList();

                    return _mapper.Map<List<OrderDTO>>(orders);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get orders for user with ID: {UserId}", userId);
            }

            return null;
        }
        public ICollection<OrderDTO> GetOrdersByCartId(int cartid)
        {
            try
            {
                var user = _context.Users
            .Include(u => u.Orders)
            .FirstOrDefault(u => u.CartId == cartid);

                if (user != null)
                {
                    var orders = _context.Orders
                        .Include(o => o.payment)
                        .Include(o => o.OrderProducts).ThenInclude (p => p.Product) 
                        .Where(o => o.ApplicationUserId == user.Id && !o.IsCancelled)
                        .ToList();

                    return _mapper.Map<List<OrderDTO>>(orders);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get orders for cart with ID: {cartid}", cartid);
            }

            return null;
        }

        public ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId)
        {
            try
            {
                var payment = _context.Payment
                .Include(p => p.Orders.Where(o => !o.IsCancelled))
                .ThenInclude(o => o.OrderProducts).ThenInclude(p => p.Product)
                .FirstOrDefault(p => p.PaymentId == paymentId);

                return _mapper.Map<List<OrderDTO>>(payment.Orders);

            }
            catch
            {
                return null;
            }
        }

        public OrderDTO CreateOrder(int cartId, int paymentId)
        {
            var cart = _context.Carts.Include(c => c.ProductsInCart).FirstOrDefault(c => c.CartId == cartId);
            var payment = _context.Payment.Find(paymentId);

            if (cart == null || payment == null)
            {
                // Handle invalid cart or payment
                return null;
            }

            // Create the order
            var order = new Order
            {
                CreatedAt = DateTime.Now,
                ShippingDate = DateTime.Now.AddDays(3),
                Total = 0, // Calculate total below
                IsCancelled = false,
                ApplicationUser = cart.ApplicationUser,
                ApplicationUserId = cart.ApplicationUserId,
                //payment = payment,
                PaymentId = payment.PaymentId,
            };

            List<OrderProduct> OrderProducts = new List<OrderProduct>();

            foreach (var item in cart.ProductsInCart)
            {
                var product = _context.Products.Find(item.ProductId);

                if (product != null)
                {
                    // Create an OrderProduct
                    var orderProduct = new OrderProduct
                    {
                        ProductId = product.ProductId,
                        Order = order,
                        OrderId = order.OrderId,
                        Product = product,
                        Quantity = item.Quantity
                    };

                    // Add to order
                    order.OrderProducts.Add(orderProduct);

                    // Calculate total
                    order.Total += (int)item.Quantity * (int)product.Price;
                    product.Stock = product.Stock- item.Quantity;
                }

            }
            // Save changes
            _context.Orders.Add(order);
            _context.SaveChanges();

    
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
