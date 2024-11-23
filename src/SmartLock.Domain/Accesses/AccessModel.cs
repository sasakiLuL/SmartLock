using SmartLock.Core.Entities;
using SmartLock.Domain.Locks.LockIds;
using SmartLock.Domain.Users.UserIds;

namespace SmartLock.Domain.Accesses;

public class AccessModel : IModel
{
    public required UserId UserId { get; init; }

    public required LockId LockId { get; init; }

    public required AccessRole Role { get; set; }

    public required DateTime CreatedOnUtc { get; init; }
}
