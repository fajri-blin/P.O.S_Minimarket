using API.Contracts.Repositories;
using API.Data;

namespace API.Repository;

public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
    where TEntity : class
{
    protected readonly PosDbContext _posDbContext;

    public GeneralRepository(PosDbContext posDbContext)
    {
        _posDbContext = posDbContext;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _posDbContext.Set<TEntity>().ToList();
    }

    public TEntity? GetByGuid(Guid guid)
    {
        var entity = _posDbContext.Set<TEntity>().Find(guid);
        _posDbContext.ChangeTracker.Clear();
        return entity;
    }

    public TEntity? Create(TEntity entity)
    {
        try
        {
            _posDbContext.Set<TEntity>().Add(entity);
            _posDbContext.SaveChanges();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            _posDbContext.Set<TEntity>().Update(entity);
            _posDbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Delete(TEntity entity)
    {
        try
        {
            _posDbContext.Set<TEntity>().Remove(entity);
            _posDbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool IsExits(Guid guid)
    {
        return GetByGuid(guid) != null;
    }

}
