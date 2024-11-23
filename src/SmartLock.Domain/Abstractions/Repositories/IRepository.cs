using SmartLock.Core.Entities;

namespace SmartLock.Domain.Abstractions.Repositories;

public interface IRepository<TValue, TModel>
    where TValue : Entity<TModel>
        where TModel : IModel
{
    Task AddAsync(TValue entity, CancellationToken token = default);

    void Delete(TValue entity);
}
