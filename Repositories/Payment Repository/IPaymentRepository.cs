using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.Payment_Repository;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    public List<Payment> GetAllPayments();
    public Payment GetAllPaymentsById(int id);
    public void Create(Payment payment);



}