using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartLock.Domain.Entities;

namespace SmartLock.DataAccessLayer.Interceptors;

public class PublishDomainEventsInterceptor(
    IMediator mediator) : SaveChangesInterceptor
{
    private readonly List<AggregateRoot> _aggregateRoots = [];

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not SmartLockContext context)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        _aggregateRoots.AddRange(context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity));

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var domainEvents = _aggregateRoots.SelectMany(a => a.DomainEvents).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }

        _aggregateRoots.ForEach(e => e.ClearDomainEvents());
        _aggregateRoots.Clear();

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}