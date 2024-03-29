namespace ECommerce.DTOS.Cart;

public class CartUpdateDto
{
    public int CartId { get; init; }
    public List<int>  ProductId { get; set; }
}