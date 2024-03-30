using ECommerce.DTOS.SubCategory;

namespace ECommerce.DTOS.Category
{
    public record CategoryReadDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual List<SubCategoryChildReadDTO> SubCategories { get; set; }

    }
}
