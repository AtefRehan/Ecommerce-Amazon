using ECommerce.DTOS.Product;

namespace ECommerce.DTOS.ProductInCart;

public record ChildProductInCartDto
{
    public int Id { get; init; }
    public ProductDTO Product { get; init; }
    public int Quantity { get; set; }

}