namespace API.Contracts.Repositories;

public interface IGeneralRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    bool IsExits(Guid guid);
}
