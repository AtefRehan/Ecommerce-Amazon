using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Wish_Repository
{
    public interface IWishRepository: IGenericRepository<WishProduct>
    {
        public WishProduct AddProduct(int product_id,string userId);
        public WishProduct RemoveProduct(int product_id, string userId);
        public IEnumerable<Product> GetWishProductsByUserId(string userId);
    }
}
