using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Product_Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    public List<Product> GetAllProducts();
    public Product GetProductById(int id);
    public void Create(Product _product);
    public void DeleteProductById(int id);

}