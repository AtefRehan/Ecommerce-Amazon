using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.Catergory_Repository;

public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
{
    private readonly AmazonDB _context;

    public CategoryRepository(AmazonDB context) : base(context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.Categories.Include(a => a.SubCategories).ToList();
    }

    public Category GetAllCategoriesById(int id)
    {
        return _context.Categories
                       .Include(p => p.SubCategories)
                       .FirstOrDefault(p => p.CategoryId == id);
    }

    public void Create(Category category)
    {
        _context.Categories.Add(category);
        SaveChanges();
    }


    public void DeleteCategoryById(int id)
    {
        var targetCategory = _context.Categories.Include(p => p.SubCategories).FirstOrDefault(p => p.CategoryId == id);

        if (targetCategory != null)
        {
            _context.SubCategories.RemoveRange(targetCategory.SubCategories);

            _context.Categories.Remove(targetCategory);
            _context.SaveChanges();
        }
    }
}