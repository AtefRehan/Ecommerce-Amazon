using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOS.Product
{
    public record ProductUpdateDTO
    {
        public int productId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number.")]
        public int Stock { get; set; }
        public string Image { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public float Weight { get; set; }
        public int SupplierId { get; set; }
        public int SubCategoryId { get; set; }
    }
}