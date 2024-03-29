namespace ECommerce.DTOS.Product
{
    public record ProductDetailsDTO(int ProductId, string Name, int? Price, int Stock, string Image, string Description, int Weight);
}
