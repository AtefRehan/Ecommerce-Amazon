using ECommerce.DTOS.Product;

namespace ECommerce.DTOS.Supplier
{
    public record SupplierDTO {

        public int supplierId {  get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<ChildProductDTO> Products { get; init; }


    }

}
