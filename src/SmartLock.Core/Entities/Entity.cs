using SmartLock.Core.Events;

namespace SmartLock.Core.Entities;

public abstract class Entity<TModel>(TModel model) where TModel : IModel
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    public TModel Model { get; init; } = model;

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
}
