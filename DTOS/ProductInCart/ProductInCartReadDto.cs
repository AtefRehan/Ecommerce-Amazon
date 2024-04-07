using ECommerce.DTO;
using ECommerce.DTOS.Cart;
using ECommerce.DTOS.Product;

namespace ECommerce.DTOS.ProductInCart
{
    public record ProductInCartReadDto
    {
        public int Id { get; init; }
        public ProductDTO Product { get; init; }
        // public CartDto Cart { get; init; }
        public int Quantity { get; init; }
    }
}
