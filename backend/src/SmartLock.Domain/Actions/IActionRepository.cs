using SmartLock.Domain.Core;

namespace SmartLock.Domain.Actions;

public interface IActionRepository : IRepository<Action, ActionModel>
{
    Task<Action?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<List<Action>> ReadAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
