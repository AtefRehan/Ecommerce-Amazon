using ECommerce.DTOS.Order;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Order_Repository;

public interface IOrderRepository: IGenericRepository<Order>
{
    ICollection<OrderDTO> GetOrdersByUserId(string userId);
    ICollection<OrderDTO> GetOrdersByPaymentId(int paymentId);
    OrderDTO GetOrder(int orderId);
    Order DeleteOrder(int orderId);
    ICollection<OrderDTO> GetOrders();
}