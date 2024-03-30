using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories.Catergory_Repository;

public class CategoryRepository : GenericRepository<CategoryRepository> , ICategoryRepository
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
}