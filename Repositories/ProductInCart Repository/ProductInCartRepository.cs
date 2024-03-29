using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.ProductInCart_Repository;

public class ProductInCartRepository : GenericRepository<ProductInCart>, IProductInCartRepository
{
    private readonly AmazonDB _context;

    public ProductInCartRepository(AmazonDB context) : base(context)
    {
        this._context = context;
    }


    public void DeleteProductsInCartByProductId(int productId)
    {
        var productInCart = _context.ProductInCart.Find(productId);
        _context.Remove(productInCart);
        SaveChanges();
    }

    public void DeleteProductsInCartByCartId(int cartId)
    {
        var productsInCart = _context.ProductInCart.Where(i => i.CartId == cartId).ToList();
        foreach (var productInCart in productsInCart)
        {
            _context.ProductInCart.Remove(productInCart);
        }
    }

    public List<ProductInCart> GetProductsInCartByCartId(int cartId)
    {
        var productsInCart = _context.ProductInCart.Where(i => i.CartId == cartId).ToList();
        return productsInCart;
    }

    public ProductInCart GetProductInCartById(int id )
    {
        return _context.ProductInCart.FirstOrDefault(c => c.ProductId == id);
    }
}