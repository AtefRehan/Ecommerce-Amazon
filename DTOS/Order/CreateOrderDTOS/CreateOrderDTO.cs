namespace ECommerce.DTOS.Order.CreateOrderDTOS
{
    public record CreateOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ShippingDate { get; set; }
        public int Total { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        public List<ProductInOrderDTO> Items { get; set; }
        public string CardType { get; set; }
        public int? Card_Num { get; set; }

    }
}
