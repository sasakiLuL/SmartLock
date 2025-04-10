namespace SmartLock.Domain.Core;

public interface IRepository<TEntity, TModel> where TEntity : Entity<TModel> where TModel : Model
{
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    public void Delete(TEntity entity);
}
