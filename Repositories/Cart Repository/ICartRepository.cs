using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Cart_Repository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        void Add(Cart cart);
        List<Cart> GetAllCarts();
        Cart CartByUserId(string userid);
        void DeleteById(int Id);


    }
}