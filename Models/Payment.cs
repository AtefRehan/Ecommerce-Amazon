using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Payment
    {

        [Key]
        public int PaymentId { get; set; }

        [MaxLength(20)]
        public string Card_Num { get; set; }

        [Required]
        [StringLength(50)]
        public required string CardType { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ExpireDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
