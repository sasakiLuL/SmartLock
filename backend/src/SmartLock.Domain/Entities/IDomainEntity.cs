namespace SmartLock.Domain.Entities;

public interface IDomainEntity<TModel> where TModel : Entity
{
    TModel Model { get; }
}
