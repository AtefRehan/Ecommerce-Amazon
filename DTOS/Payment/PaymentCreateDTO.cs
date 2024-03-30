using ECommerce.Models;
namespace ECommerce.DTOS.Payment
{
    public record PaymentCreateDTO
    {
        public int Card_Num { get; set; }

        public string CardType { get; set; }

        public DateTime ExpireDate { get; set; }

    }
}
