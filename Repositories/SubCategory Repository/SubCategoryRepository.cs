using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using ECommerce.Repositories.Product_Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.SubCategory_Repository;

public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
{
    private readonly AmazonDB _context;
    // private readonly IProductRepository _productRepo;

    public SubCategoryRepository(IProductRepository productRepo, AmazonDB context) : base(context)
    {
        // this._productRepo = productRepo;
        _context = context;
    }
    
    public List<SubCategory> GetAll()
    {
        return _context.SubCategories.Include(c=>c.Category)
            .Include(c=>c.Products).ToList();
    }

    public SubCategory GetById(int id)
    {
        return  _context.SubCategories.Include(c => c.Category).Include(c => c.Products)
            .FirstOrDefault(c => c.SubCategoryId == id);
    }

    public void DeleteSubCategoriesByCategoryId(int categoryId)
    {
        var subCategories = _context.SubCategories.Where(c => c.Category.CategoryId == categoryId).ToList();
        foreach (var subCategory in subCategories)
        {
            var products = _context.Products.Where(c => c.SubCategoryId == subCategory.SubCategoryId).ToList();
            foreach (var product in products)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }


            _context.SubCategories.Remove(subCategory);
        }
    }

    public int GetNumOfSubCategories()
    {
        return _context.SubCategories.Count();
    }
}