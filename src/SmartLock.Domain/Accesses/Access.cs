using SmartLock.Core.Entities;
using SmartLock.Core.Results;

namespace SmartLock.Domain.Accesses;

public class Access(AccessModel model) : Entity<AccessModel>(model)
{
    public Result ChangeRole(AccessRole role)
    {
        if (role == Model.Role)
        {
            return Result.Failure(AccessErrors.SameRole);
        }

        Model.Role = role;

        return Result.Success();
    }
}
