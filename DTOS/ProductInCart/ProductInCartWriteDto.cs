using System;

namespace ECommerce.DTOS.ProductInCart
{
    public record ProductInCartWriteDto
    {
        public int ProductId { get; init; }
        public int CartId { get; init; }
        public int Quantity { get; init; }
    }
}