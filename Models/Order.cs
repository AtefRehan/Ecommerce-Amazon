using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ShippingDate { get; set; }
        public int Total { get; set; }
        public bool IsCancelled { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("payment")]
        public int? PaymentId { get; set; }
        public virtual Payment payment { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }= new HashSet<OrderProduct>();
    }
}
