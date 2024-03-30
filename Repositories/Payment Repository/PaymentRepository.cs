using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Repositories.Payment_Repository;

public class PaymentRepository:GenericRepository<Payment>,IPaymentRepository
{
    private readonly AmazonDB _context;
    public PaymentRepository(AmazonDB context):base(context)
    {
        _context = context;
    }

    public List<Payment> GetAllPayments()
    {
        return _context.Payment.Include(a => a.Orders).ToList();
    }

    public Payment GetAllPaymentsById(int id)
    {
        return _context.Payment
                       .Include(p => p.Orders)
                       .FirstOrDefault(p => p.PaymentId == id);
    }

    public void Create(Payment payment)
    {
        _context.Payment.Add(payment);
        SaveChanges();
    }


}