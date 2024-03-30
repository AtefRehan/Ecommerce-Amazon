using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ECommerce.Repositories.SupplierRepository;

public class SupplierRepository:GenericRepository<Supplier>, ISupplierRepository
{
    private readonly AmazonDB _context;
    public SupplierRepository(AmazonDB context):base(context)  
    {
        _context=context;
            
    }
    public List<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.Include(a=> a.Products).ToList();
    }
    public Supplier GetAllSuppliersById(int id)
    {
        return _context.Suppliers
                       .Include(p => p.Products)
                       .FirstOrDefault(p => p.SupplierId == id);
    }

    public void Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        SaveChanges();
    }
    //public void DeleteSupplierById(int id)
    //{
    //    var supplier = _context.Suppliers.Include(p => p.Products).FirstOrDefault(p => p.SupplierId == id);

    //    _context.Suppliers.Remove(supplier);
    //    _context.SaveChanges();
    //}
    public void DeleteSupplierById(int id)
    {
        var supplier = _context.Suppliers.Include(p => p.Products).FirstOrDefault(p => p.SupplierId == id);

        if (supplier != null)
        {
            foreach (var product in supplier.Products)
            {
                var productsInCarts = _context.ProductInCart.Where(p => p.ProductId == product.ProductId).ToList();
                _context.ProductInCart.RemoveRange(productsInCarts);
            }

            _context.Products.RemoveRange(supplier.Products);

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
    }
}
