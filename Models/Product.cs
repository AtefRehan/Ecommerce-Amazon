using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerce.Models;

namespace ECommerce.Models
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }

        [MaxLength(30)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number.")]
        public int Stock { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public float Weight { get; set; }
        public bool IsCancelled { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }


        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<OrderProduct> ProductOrders { get; set; } = new HashSet<OrderProduct>();

        public virtual ICollection<WishProduct> UsersWishList { get; set; } =
        new HashSet<WishProduct>();
        public virtual ICollection<ProductInCart>? ProductInCarts { get; set; } = new HashSet<ProductInCart>();
    }
}
