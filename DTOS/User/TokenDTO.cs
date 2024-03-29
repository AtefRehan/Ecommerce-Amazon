namespace ECommerce.DTO.User
{
    public class TokenDTO
    {
        public string Token { get; init; }
        public DateTime ExpiryDate { get; set; }
    }
}
