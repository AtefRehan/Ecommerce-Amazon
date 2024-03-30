namespace ECommerce.DTOS.Order
{
    public record ChildOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime ShippingDate { get; set; }
        public int Total { get; set; }

    }
}
