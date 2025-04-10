using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Actions;
using Action = SmartLock.Domain.Actions.Action;

namespace SmartLock.DataAccessLayer.Actions;

public class ActionRepository : Repository<Action, ActionModel>, IActionRepository
{
    public ActionRepository(SmartLockContext smartLockContext) : base(smartLockContext)
    {
    }

    public async Task<Action?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var actionModel = await _context.ActionModels.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (actionModel is null)
        {
            return null;
        }

        return CreateEntityFromModel(actionModel);
    }

    public async Task<List<Action>> ReadAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.ActionModels
            .Where(x => x.UserId == userId)
            .Select(x => CreateEntityFromModel(x))
            .ToListAsync();
    }
}
