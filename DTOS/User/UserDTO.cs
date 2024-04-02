namespace ECommerce.DTO.User
{
    public class UserDTO
    {
        public string ApplicationUserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> Role { get; set; }
        public List<int>? WishList_productsId { get; set; }
        public List<int>? Orders_ID { get; set; }

    }
}
