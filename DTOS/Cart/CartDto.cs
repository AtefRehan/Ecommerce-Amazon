using System.Collections.ObjectModel;
using ECommerce.DTOS.ProductInCart;
using ECommerce.DTOS.User;
using ECommerce.Models;

namespace ECommerce.DTOS.Cart;

public record CartDto
{
    public int CartId { get; set; }
    // public ChildApplicationUserDto ApplicationUser { get; init; }
    public Collection<ChildProductInCartDto> ProductsInCart { get; set; }
    
}