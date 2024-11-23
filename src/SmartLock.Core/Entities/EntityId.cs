namespace SmartLock.Core.Entities;

public interface IEntityId
{
    Guid Value { get; init; }
}
