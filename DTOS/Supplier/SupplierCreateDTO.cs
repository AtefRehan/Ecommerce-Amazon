namespace ECommerce.DTOS.Supplier
{
    public record SupplierCreateDTO
    {
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
