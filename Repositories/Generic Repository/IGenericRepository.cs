namespace ECommerce.Repositories.Generic_Repository;

public interface IGenericRepository <TEntity>where TEntity : class
{
    List<TEntity> GetAll();
    TEntity GetById(int id);
    void Create(TEntity entity);
    void Delete(int id);
    int SaveChanges();
    void Update(TEntity entity);
}