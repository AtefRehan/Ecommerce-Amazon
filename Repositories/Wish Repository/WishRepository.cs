using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ECommerce.Repositories.Wish_Repository
{
    public class WishRepository : GenericRepository<WishProduct>, IWishRepository
    {
        private AmazonDB context;
        public WishRepository(AmazonDB _context):base(_context)
        {
            this.context = _context;
        }

        public IEnumerable<Product> GetWishProductsByUserId(string userId) 
        {
            if (userId != null)
            {
                var products = context.Products
                           .Join(context.Wish,
                                p => p.ProductId,
                                w => w.ProductId,
                               (p, w) => new { Product = p, Wish = w })
                        .Where(j => j.Wish.UserId == userId)
                        .Select(j => j.Product)
                        .ToList();

                return products;

            }
            else
            {
                return null;
            }
        }
        public  WishProduct AddProduct(int product_id ,string userID)
        {
            context.Wish.Include(p => p.Product).Include(p => p.Product);
            var product = context.Products.Find(product_id);
            var user = context.Users.FirstOrDefault(a => a.Id == userID);
            
            if (product != null&& user!=null)
            {
                WishProduct wish_product = new WishProduct()
                {
                    ProductId = product.ProductId,
                    Product = product,
                    User = user,
                    UserId = userID
                };
               
                context.Wish.Add(wish_product);
                context.SaveChanges();
                return wish_product;
            }
            else
            {
                return null;
            }
            
        }

        public WishProduct RemoveProduct(int product_id, string userID)
        {
            var wishToDelete =  context.Wish
                                .FirstOrDefault(w => w.ProductId == product_id && w.UserId == userID);

            if (wishToDelete != null)
            {
                context.Wish.Remove(wishToDelete);
                context.SaveChanges();
                return wishToDelete;
            }
            return null ;

        }

    }
}
