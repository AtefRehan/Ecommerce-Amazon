using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

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
        var productsInCart = _context.ProductInCart.Include(c=>c.Product).Include(c=>c.Cart).Where(i => i.CartId == cartId).ToList();
        return productsInCart;
    }

    public ProductInCart GetProductInCartById(int id )
    {
        return _context.ProductInCart.FirstOrDefault(c => c.ProductId == id);
    }

    public ProductInCart GetProductInCartByProductId(int ProductId)
    {
        return _context.ProductInCart.FirstOrDefault(c => c.ProductId == ProductId);
    }

    public int GetProductInCartCount(int CartId)
    {
        int count = 0;
        var cart = _context.Carts.Include(o => o.ProductsInCart).FirstOrDefault(i => i.CartId == CartId);
        var cartProducts = cart.ProductsInCart.ToList();
        foreach( var product in cartProducts)
        {
            count++;
        }
        return count;
    }

}