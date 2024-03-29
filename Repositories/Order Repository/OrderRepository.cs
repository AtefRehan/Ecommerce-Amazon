using AutoMapper;
using ECommerce.Data;
using ECommerce.DTOS.Order;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerce.Repositories.Order_Repository;

public class OrderRepository: GenericRepository<Order>,IOrderRepository
{
    private readonly AmazonDB context;
    private readonly UserManager<ApplicationUser> userManger;
    private readonly IMapper mapper;
    public OrderRepository(AmazonDB _context, UserManager<ApplicationUser> _userManager,IMapper _mapper) : base(_context)
    {
        this.context = _context;
        this.userManger = _userManager;
        this.mapper = _mapper;
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
        //var order = _context.Orders.Find(orderId);
        var o =  _context.Orders.Include(o => o.OrderProducts).FirstOrDefault(o => o.OrderId == id);
        if (o != null)
        {
            var order =
                mapper.Map<OrderDTO>(o);
                
            //    new OrderDTO()
            //{
            //    OrderId = o.OrderId,
            //    CreatedAt = o.CreatedAt,
            //    ShippingDate = o.ShippingDate,
            //    Total = o.Total,
            //    IsCancelled = o.IsCancelled,
            //    ApplicationUserId = o.ApplicationUserId,
            //    PaymentMethod = o.payment.CardType,
            //    OrderProductsID = o.OrderProducts.Select(op => op.ProductId).ToList()
            //};
            return order;
        }
        return null;
    }
    public ICollection<OrderDTO> GetOrders()
    {
        var orders = _context.Orders.Include(o => o.OrderProducts).Include(p => p.payment).ToList();
        if (orders.Count > 0)
        {
            var dtos = orders.Select(o =>
                mapper.Map<OrderDTO>(o)
               ).ToList();
            return dtos;
        }
        return null;
    }


    public Order AddOrder(OrderDTO orderDTO)
    {
            Order order = new Order
            {
                CreatedAt = orderDTO.CreatedAt,
                ShippingDate = orderDTO.ShippingDate,
                Total = orderDTO.Total,
                IsCancelled = orderDTO.IsCancelled,
                ApplicationUserId = orderDTO.ApplicationUserId,
            };
        try
        {
            foreach (var id in orderDTO.OrderProductsID)
            {

                try
                {
                    var product = _context.Products.Find(id);
                    order.OrderProducts.Add(product);
                }
                catch (Exception ex)
                {
                }

            }

            // Add the order to the context and save changes
             _context.Orders.Add(order);
             _context.SaveChanges();

            // Return the added order
            return order;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the order: {ex.Message}");
            return null;
        }
    }


    public  ICollection<OrderDTO> GetOrdersByUserId(string userId)
    {
        var user =  _context.Users.Include(o => o.Orders).ThenInclude(p => p.payment).FirstOrDefault(o => o.Id == userId);
        //var user = _context.Users.Include(o => o.Orders).ThenInclude(p => p.Payments).FirstOrDefault(o => o.Id == userId);

        if (user != null)
        {
            return user.Orders.Select(o =>mapper.Map<OrderDTO>(o)
            //new OrderDTO()
            //{
            //    OrderId = o.OrderId,
            //    CreatedAt = o.CreatedAt, // Include other properties as needed
            //    ShippingDate = o.ShippingDate,
            //    Total = o.Total,
            //    IsCancelled = o.IsCancelled,
            //    ApplicationUserId = o.ApplicationUserId,
            //    PaymentMethod = o.payment.CardType,
            //    OrderProductsID = o.OrderProducts.Select(op => op.ProductId).ToList()
            //}
            ).ToList();
        }
        return null;
    }




    public ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId)
    {
        var payment =  _context.Payment.Include(o => o.Orders).FirstOrDefault(p => p.PaymentId == paymentId);
        if (payment != null)
        {
            var o = payment.Orders.ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (var item in o)
            {
                OrderDTO orderDTO =mapper.Map<OrderDTO>(item);
                //    new OrderDTO()
                //{
                //    OrderId = item.OrderId,
                //    CreatedAt = item.CreatedAt, // Include other properties as needed
                //    ShippingDate = item.ShippingDate,
                //    Total = item.Total,
                //    IsCancelled = item.IsCancelled,
                //    ApplicationUserId = item.ApplicationUserId,
                //    PaymentMethod = item.payment.CardType,
                //    OrderProductsID = item.OrderProducts.Select(op => op.ProductId).ToList()
                //};
                orderDTOs.Add(orderDTO);
                 _context.SaveChanges();
            }
            return orderDTOs;
        }
        return null;
    }


    //Order AddOrder(OrderDTO orderDTO)
    //{ var products = _context.Products.ToList();
    //    Order order = new Order();
    //    foreach (var item in orderDTO.OrderProductsID)
    //    {
    //        Order products.Select(o => o.ProductId ==item).ToList();

    //    }
    //    order.OrderProducts =
    //    return orders.Select(o => new OrderDTO()
    //    {
    //        OrderId = o.OrderId,
    //        CreatedAt = o.CreatedAt,
    //        ShippingDate = o.ShippingDate,
    //        Total = o.Total,
    //        IsCancelled = o.IsCancelled,
    //        ApplicationUserId = o.ApplicationUserId,
    //        PaymentMethod = o.payment.CardType,
    //        OrderProductsID = o.OrderProducts.Select(op => op.ProductId).ToList()
    //    return null;
    //}


    //public Order GetOrderByUserId(string userId)
    //{
    //    var order = _context.Orders
    //        .Include(o => o.ApplicationUser)  // Assuming there's a navigation property from Order to User
    //        .LastOrDefault(o => o.ApplicationUser.Id == userId);
    //    return order;
    //}

    //public List<Order> GetAllOrderByUserId(string userId)
    //{
    //   return _context.Users.Find(userId).Orders.ToList();
    //}

    //public void CancelOrderOrderId(int orderId)
    //{
    //    var order=_context.Orders.Find(orderId);
    //    order.IsCancelled = true;
    //    SaveChanges();
    //}


    //public Order GetOrderByUserId(string userId)
    //{
    //    var order = _context.Orders
    //        .Include(o => o.ApplicationUser)  // Assuming there's a navigation property from Order to User
    //        .LastOrDefault(o => o.ApplicationUser.Id == userId);
    //    return order;
    //}

    //public List<Order> GetAllOrderByUserId(string userId)
    //{
    //   return _context.Users.Find(userId).Orders.ToList();
    //}



}