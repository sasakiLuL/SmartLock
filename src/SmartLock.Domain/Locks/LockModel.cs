using SmartLock.Core.Entities;
using SmartLock.Domain.Locks.LockIds;
using SmartLock.Domain.Locks.LockNames;
using SmartLock.Domain.Locks.MacAddresses;
using SmartLock.Domain.Users.UserIds;

namespace SmartLock.Domain.Locks;

public class LockModel : IModel
{
    public required LockId Id { get; init; }

    public required MacAddress MacAddress { get; init; }

    public required LockName LockName { get; set; }

    public required UserId OwnerId { get; set; }
}
