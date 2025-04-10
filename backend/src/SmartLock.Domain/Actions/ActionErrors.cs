using SmartLock.Domain.Core;

namespace SmartLock.Domain.Actions;

public static class ActionErrors
{
    public static Error NotFound(Guid id) => new Error(
        "Action.NotFound",
        $"The action with identifier: {id} was not found.");
}
