using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Features.Devices.Actions;

namespace SmartLock.DataAccessLayer;

public class UnitOfWork(SmartLockContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in context.ChangeTracker.Entries<ActionModel>())
        {
            Console.WriteLine($"ActionModel Id: {entry.Entity.Id}, State: {entry.State}");
        }
        await context.SaveChangesAsync(cancellationToken);
    }
}
