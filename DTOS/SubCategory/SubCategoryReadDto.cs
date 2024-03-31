using ECommerce.DTOS.Category;
using ECommerce.DTOS.Product;

namespace ECommerce.DTOS.SubCategory;

public record SubCategoryReadDto
{
    public int SubCategoryId { get; init; }
    public string SubCategoryName { get; init; }
    // public string Description { get; init; }
    public ChildCategoryReadDto Category { get; init; }
    public List<ProductDTO> Products { get; init; }
    // public List<ChildProductDTO> Products { get; init; }
}