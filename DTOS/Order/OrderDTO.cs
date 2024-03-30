namespace ECommerce.DTOS.Order
{
    public record OrderDTO
    {
        public int OrderId { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime ShippingDate { get; set; }
        public int Total { get; set; }
        public bool IsCancelled { get; set; }
        public string ApplicationUserId { get; set; }
        public List<int> OrderProductsID { get; set; }
    }
}
