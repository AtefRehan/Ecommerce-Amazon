using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.Product_Repository;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private AmazonDB _context;
    public ProductRepository(AmazonDB context) : base(context)
    {
        this._context = context;
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products
                       .Include(p => p.Supplier)
                       .Include(p => p.SubCategory)
                       .FirstOrDefault(p => p.ProductId == id);
    }


    public void UpdateProductById(int id, Product p)
    {
        var product = _context.Products.Find(id);
        // _context.Entry(product).State = EntityState.Modified;
        _context.Update(product);

    }
    public void Create(Product entity)
    {

        _context.Products.Add(entity);
        _context.SaveChanges();
    }
    public void DeleteProductById(int id)
    {
        var product = _context.Products.Find(id);

        _context.Products.Remove(product);
        _context.SaveChanges();
    }


}