using SmartLock.Domain.Entities;

namespace SmartLock.Domain.Interfaces;

public interface IRepository<TEntity, TModel> 
    where TEntity : IDomainEntity<TModel> 
    where TModel : Entity
{
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public void Delete(TEntity entity);
}
