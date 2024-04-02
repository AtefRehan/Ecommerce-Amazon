using ECommerce.DTOS.Order.CreateOrderDTOS;
using ECommerce.DTOS.Order.ShowOrderDTOs;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Order_Repository;

public interface IOrderRepository: IGenericRepository<Order>
{
    ICollection<OrderDTO> GetOrdersByUserId(string userId);
    ICollection<OrderDTO> GetOrdersByCartId(int cartid);
    ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId);
    OrderDTO GetOrder(int orderId);
    ICollection<OrderDTO> GetOrders();
    OrderDTO DeleteOrder(int orderId);
    OrderDTO CreateOrder(int cartId, int paymentId);
}