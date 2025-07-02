using SmartLock.Domain.Exceptions;

namespace SmartLock.Domain.Features.Devices.Actions;

public static class ActionErrors
{
    public static Error NotFound(Guid id) => new(
        "Action.NotFound",
        $"The action with identifier: {id} was not found.");

    public static Error ActionAlreadyExecuted(Guid id) => new(
        "Action.ActionAlreadyExecuted",
        $"The action with identifier: {id} is already executed.");
}
