using SmartLock.Domain.Abstractions.Repositories;

namespace SmartLock.Domain.Locks;

public interface ILockRepository : IRepository<Lock, LockModel>
{

}
