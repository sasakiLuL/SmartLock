using SmartLock.Core.Errors;

namespace SmartLock.Domain.Locks.LockNames;

public class LockNameErrors
{
    public static readonly Error TooLong = new(
        "LockName.TooLong", "The lock name is too long.");

    public static readonly Error InvalidFormat = new(
        "LockName.InvalidFormat ", "The lock name contains unacceptable symbols.");
}
