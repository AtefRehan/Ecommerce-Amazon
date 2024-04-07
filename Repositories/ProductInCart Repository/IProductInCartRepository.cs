using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.ProductInCart_Repository;

public interface IProductInCartRepository : IGenericRepository<ProductInCart>
{
    void DeleteProductsInCartByProductId(int productId);
    void DeleteProductsInCartByCartId(int cartId);
    List<ProductInCart> GetProductsInCartByCartId(int cartId);
    // int GetProductInCartIdByCartIdAndProductId(int cartId, int productId);
    public ProductInCart GetProductInCartById(int id);
    public ProductInCart GetProductInCartByProductId(int id);
    public int GetProductInCartCount(int CartId);


}