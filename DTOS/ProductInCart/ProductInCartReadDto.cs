using ECommerce.DTO;
using ECommerce.DTOS.Product;

namespace ECommerce.DTOS.ProductInCart
{
    public record ProductInCartReadDto
    {
        //public Guid Id { get; init; }
        public ProductDTO Product { get; init; }
        public ChildCartDto Cart { get; init; }
        public int Quantity { get; init; }
    }
}
