namespace ECommerce.DTOS.Order.CreateOrderDTOS
{
    public class ProductInOrderDTO
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public required string Name { get; set; }
        public int? Price { get; set; }
        public int? TotalPrice { get; set; }

    }
}
