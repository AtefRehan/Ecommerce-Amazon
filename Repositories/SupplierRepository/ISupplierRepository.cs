using ECommerce.Models;
using ECommerce.Repositories.Generic_Repository;

namespace ECommerce.Repositories.SupplierRepository;

public interface ISupplierRepository : IGenericRepository<Supplier>
{
    public List<Supplier> GetAllSuppliers();
    public Supplier GetAllSuppliersById(int id);
    public void Create(Supplier supplier);

    public void DeleteSupplierById(int id);



}