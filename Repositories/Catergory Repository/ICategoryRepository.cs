using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Catergory_Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    public List<Category> GetAllCategories();
    public Category GetAllCategoriesById(int id);
    public void Create(Category category);
    public void DeleteCategoryById(int id);





}