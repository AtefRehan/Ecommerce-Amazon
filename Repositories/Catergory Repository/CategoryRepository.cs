using ECommerce.Data;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Catergory_Repository;

public class CategoryRepository : GenericRepository<CategoryRepository> , ICategoryRepository
{
    private readonly AmazonDB _context;

    public CategoryRepository(AmazonDB context) : base(context)
    {
        _context = context;
    }
}