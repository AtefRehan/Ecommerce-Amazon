using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace ECommerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<WishProduct> WishList { get; set; } = new HashSet<WishProduct>();
    }
}
    