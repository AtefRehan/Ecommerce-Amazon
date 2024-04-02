namespace ECommerce.DTOS.Order.CreateOrderDTOS
{
    public class CreateOrderInputDTO
    {
        public int CartId { get; set; }
        public int PaymentId { get; set; }
    }
}
