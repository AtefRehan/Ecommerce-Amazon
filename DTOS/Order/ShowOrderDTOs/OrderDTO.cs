using ECommerce.DTOS.Order.CreateOrderDTOS;

namespace ECommerce.DTOS.Order.ShowOrderDTOs
{
    public record OrderDTO
    {
        public int OrderId { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime ShippingDate { get; set; }
        public int Total { get; set; }
        public bool IsCancelled { get; set; }
        public string ApplicationUserId { get; set; }
        public string CardType { get; set; }
        public List<ProductInShowOrder> OrderProducts { get; set; }
    }
}
