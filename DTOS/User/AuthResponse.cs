namespace ECommerce.DTOS.User
{
    public record AuthResponse
    {
        public string UserId { get; set; }

        public int? CartId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }

    }

}
