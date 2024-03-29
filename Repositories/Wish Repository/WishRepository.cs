using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
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

        public  WishProduct AddProduct(int product_id ,string userID)
        {
            var product = context.Products.Find(product_id);
            var user = context.Users.Find(userID);
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

            return null;
        }

        public WishProduct RemoveProduct(int product_id)
        {

            WishProduct product_wish = context.Wish.FirstOrDefault(w => w.ProductId == product_id);
            if (product_wish != null) { context.Wish.Remove(product_wish); }
            return null;

        }
    }
}
