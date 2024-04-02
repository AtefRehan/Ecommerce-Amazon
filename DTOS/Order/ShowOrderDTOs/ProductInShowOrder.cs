namespace ECommerce.DTOS.Order.ShowOrderDTOs
{
    public class ProductInShowOrder
    {
        public int ProductId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }

    }
}
