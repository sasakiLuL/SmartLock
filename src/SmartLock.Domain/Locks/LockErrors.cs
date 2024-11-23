using SmartLock.Core.Errors;

namespace SmartLock.Domain.Locks;

public static class LockErrors
{
    public static Error NotFound(Guid id) => new("Lock.NotFound", $"The lock with id = {id} was not found.");
}
