namespace ECommerce.DTOS.Supplier
{
    public class SupplierUpdateDTO
    {
        public int supplierId { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> ProductsId { get; init; }

    }
}
