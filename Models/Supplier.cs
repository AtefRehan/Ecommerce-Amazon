using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        public string? SupplierName { get; set; }

        [EmailAddress]
        public string Email { set; get; }

        [MaxLength(100)]
        public string Address { set; get; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Product> Products { get; set; } =
            new HashSet<Product>();
    }
}