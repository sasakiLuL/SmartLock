using SmartLock.Domain.Entities;
using SmartLock.Domain.Interfaces;

namespace SmartLock.DataAccessLayer;

public abstract class Repository<TEntity, TModel> : IRepository<TEntity, TModel>
   where TEntity : IDomainEntity<TModel>
   where TModel : Entity
{
    protected readonly SmartLockContext _context;

    protected Repository(SmartLockContext smartLockContext)
    {
        _context = smartLockContext;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TModel>().AddAsync(entity.Model, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TModel>().Remove(entity.Model);
    }
}
