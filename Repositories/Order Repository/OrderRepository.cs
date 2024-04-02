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
                var order = _context.Orders.Where(o => !o.IsCancelled).Include(o => o.OrderProducts).FirstOrDefault(o => o.OrderId == orderId);
                order.IsCancelled = true;
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
                var order = _context.Orders.Where(o => !o.IsCancelled)
                .Include(o => o.OrderProducts)
                .Include(p => p.payment)
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
                var orders = _context.Orders.Where(o => !o.IsCancelled)
                .Include(o => o.OrderProducts)
                .Include(p => p.payment)
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
                        .Include(o => o.OrderProducts)
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

        public ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId)
        {
            try
            {
                var payment = _context.Payment
                .Include(p => p.Orders.Where(o => !o.IsCancelled)).ThenInclude(o => o.OrderProducts)
                .FirstOrDefault(p => p.PaymentId == paymentId);

                return _mapper.Map<List<OrderDTO>>(payment.Orders);

            }
            catch
            {
                return null;
            }

        }


        public CreateOrderDTO CreateOrder(int cartId, int paymentId)
        {
            CreateOrderDTO newOrderDTO = new CreateOrderDTO();
            List<ProductInOrderDTO> productsDTO = new List<ProductInOrderDTO>(); // Initialize the list
            Cart cart;
            Payment payment;
            Order order = new Order();
            order.OrderProducts = new List<Product>(); // Initialize the collection

            int total = 0;
            try
            {
                cart = _context.Carts.Include(p => p.ApplicationUser).Include(p => p.ProductsInCart)
                                    .FirstOrDefault(c => c.CartId == cartId);

                payment = _context.Payment.FirstOrDefault(p => p.PaymentId == paymentId);
                if (cart != null)
                {
                    order.OrderProducts = new List<Product>(); // Initialize the collection
                    foreach (var item in cart.ProductsInCart)
                    {
                        var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);

                        if (product != null)
                        {
                            order.OrderProducts.Add(product);

                            int quantity = item.Quantity;
                            int? price = product.Price;

                            if (price != null)
                            {
                                total += quantity * price.Value;
                                var productOrderDTO = new ProductInOrderDTO
                                {
                                    Name = product.Name,
                                    Image = product.Image,
                                    ProductId = item.ProductId,
                                    Quantity = quantity,
                                    Price = price,
                                    TotalPrice = quantity * price.Value
                                };
                                productsDTO.Add(productOrderDTO);
                            }
                        }
                    }

                    if (payment != null)
                    {
                        order.CreatedAt = DateTime.Now;
                        order.ShippingDate = DateTime.Now.AddDays(3);
                        order.IsCancelled = false;
                        order.ApplicationUser = cart.ApplicationUser;
                        order.ApplicationUserId = cart.ApplicationUserId;
                        order.payment = payment;
                        order.PaymentId = payment.PaymentId;
                        order.Total = total;
                        _context.Orders.Add(order);
                        _context.SaveChanges();
                        newOrderDTO.UserName = order.ApplicationUser.UserName;
                        newOrderDTO.OrderId = order.OrderId;
                        newOrderDTO.ApplicationUserId = order.ApplicationUserId;
                        newOrderDTO.CreatedAt = order.CreatedAt;
                        newOrderDTO.ShippingDate = order.ShippingDate;
                        newOrderDTO.Items = productsDTO;
                        newOrderDTO.Total = total;
                        newOrderDTO.CardType = payment.CardType;
                        newOrderDTO.Card_Num = payment.Card_Num;
                    }
                }

                return newOrderDTO;
            }
            catch (Exception ex) { _logger.LogError(ex, "Can't Fetch Data"); return null; }

        }

    }
}
