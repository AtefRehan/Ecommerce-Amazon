using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class WishProduct
    {

        // Foreign key for ApplicationUser (User)
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        // Navigation property to ApplicationUser (User)
        public virtual ApplicationUser User { get; set; }

        // Foreign key for Product
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        // Navigation property to Product
        public virtual Product Product { get; set; }
    }
}
