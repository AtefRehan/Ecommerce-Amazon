namespace ECommerce.DTOS.Product
{
    public record ProductDTO(int ProductId, string Name, int ?Price, int Stock, string Image);

}
