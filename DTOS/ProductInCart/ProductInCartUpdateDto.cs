using System;

namespace ECommerce.DTOS.ProductInCart
{
    public record ProductInCartUpdateDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid CartId { get; init; }
        public int Quantity { get; init; }
    }
}