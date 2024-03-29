using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<ProductInCart>? ProductsInCart { get; set; }=new HashSet<ProductInCart>(); 

    }
}
