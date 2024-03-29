using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Payment_Repository;

public class PaymentRepository:GenericRepository<Payment>,IPaymentRepository
{
    private readonly AmazonDB _context;
    public PaymentRepository(AmazonDB context):base(context)
    {
        _context = context;
    }

}