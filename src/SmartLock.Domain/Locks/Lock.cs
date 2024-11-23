using SmartLock.Core.Entities;
using SmartLock.Core.Results;
using SmartLock.Domain.Locks.LockNames;

namespace SmartLock.Domain.Locks;

public class Lock(LockModel model) : Entity<LockModel>(model)
{
    public Result ChangeLockName(LockName lockName)
    {
        Model.LockName = lockName;

        return Result.Success();
    }
}
