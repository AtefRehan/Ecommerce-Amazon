using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.Cart_Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly AmazonDB _context;

        public CartRepository(AmazonDB context) : base(context)
        {
            _context = context;
        }

        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public List<Cart> GetAllCarts()
        {
            // return _context.Carts.Include(a => a.ProductsInCart).ToList();
            var carts = _context.Carts.Include(c => c.ApplicationUser)
                .Include(c => c.ProductsInCart).ThenInclude(c => c.Product)
                .ToList();
            return carts;
        }

        public Cart GetById(int id)
        {
            var cart = _context.Carts.Include(c => c.ApplicationUser)
                .Include(c => c.ProductsInCart).ThenInclude(c => c.Product).FirstOrDefault(c => c.CartId == id);
            return cart;
        }
        public Cart CartByUserId(string userid)
        {
            return _context.Carts.FirstOrDefault(i => i.ApplicationUser.Id == userid);
        }

        public void DeleteById(int id)
        {
           Delete(id);
           SaveChanges();
        }
    }
}