namespace ECommerce.DTOS.Product
{
    public record ProductUpdateDTO
    {
        public int productId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public float Weight { get; set; }
        public int SupplierId { get; set; }
        public int SubCategoryId { get; set; }
    }
}