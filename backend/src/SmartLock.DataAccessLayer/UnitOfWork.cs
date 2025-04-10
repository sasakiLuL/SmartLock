using SmartLock.Application.Abstractions;

namespace SmartLock.DataAccessLayer;

public class UnitOfWork(SmartLockContext smartLock) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await smartLock.SaveChangesAsync(cancellationToken);
    }
}
