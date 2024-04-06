using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.AspNetCore.Http.HttpResults;
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
        return _context.Products.Where(a => !a.IsCancelled).ToList();
    }

    public Product GetProductById(int id)
    {
        return _context.Products.Where(a => !a.IsCancelled)
                       .Include(p => p.Supplier)
                       .Include(p => p.SubCategory)
                       .FirstOrDefault(p => p.ProductId == id);
    }

    public void Create(Product entity)
    {

        _context.Products.Add(entity);
        _context.SaveChanges();
    }
    public void DeleteProductById(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null) {
            product.IsCancelled = true;
            _context.SaveChanges();
        }
        
    }


}