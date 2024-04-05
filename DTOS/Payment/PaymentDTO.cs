using ECommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerce.DTOS.Product;
using ECommerce.DTOS.Order;

namespace ECommerce.DTOS.Payment
{
    public record PaymentDTO
    {
        public int PaymentId { get; set; }
        public string Card_Num { get; set; }
        public string CardType { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ExpireDate { get; set; }
        public List<ChildOrderDTO> Orders { get; init; }

    }
}
