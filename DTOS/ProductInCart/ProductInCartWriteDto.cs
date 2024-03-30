using System;

namespace ECommerce.DTOS.ProductInCart
{
    public record ProductInCartWriteDto
    {
        public Guid ProductId { get; init; }
        public Guid CartId { get; init; }
        public int? Quantity { get; init; }
    }
}