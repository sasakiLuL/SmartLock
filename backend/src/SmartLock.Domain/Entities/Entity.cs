namespace SmartLock.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; internal init; }
}
