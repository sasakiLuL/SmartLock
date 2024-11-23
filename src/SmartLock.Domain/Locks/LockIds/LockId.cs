using SmartLock.Core.Entities;

namespace SmartLock.Domain.Locks.LockIds;

public record LockId(Guid Value) : IEntityId;
