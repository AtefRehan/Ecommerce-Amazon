using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.SubCategory_Repository;

public interface ISubCategoryRepository : IGenericRepository<SubCategoryRepository>
{
    void DeleteSubCategoriesByCategoryId(int categoryId);
    // List<SubCategory> GetAllCategoriesSorted(CategoryParameters categoryParameters);
    int GetNumOfSubCategories();
}