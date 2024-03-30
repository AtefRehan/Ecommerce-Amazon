using AutoMapper;
using ECommerce.Data;
using ECommerce.DTOS.Order;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        public Order DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.IsCancelled = true;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            return order;
        }

        public OrderDTO GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderProducts)
                .Include(p => p.payment)
                .FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                return _mapper.Map<OrderDTO>(order);
            }

            return null;
        }

        public ICollection<OrderDTO> GetOrders()
        {
            var orders = _context.Orders
                .Include(o => o.OrderProducts)
                .Include(p => p.payment)
                .ToList();

            if (orders.Count > 0)
            {
                return _mapper.Map<List<OrderDTO>>(orders);
            }

            return null;
        }

        public ICollection<OrderDTO> GetOrdersByUserId(string userId)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.Orders)
                        .ThenInclude(o => o.payment)
                    .FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    return _mapper.Map<List<OrderDTO>>(user.Orders);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get orders for user with ID: {UserId}", userId);
            }

            return null;
        }

        public ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId)
        {
            var payment = _context.Payment
                .Include(p => p.Orders)
                .FirstOrDefault(p => p.PaymentId == paymentId);

            if (payment != null)
            {
                return _mapper.Map<List<OrderDTO>>(payment.Orders);
            }

            return null;
        }

       
    }
}
