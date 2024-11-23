using SmartLock.Core.Errors;

namespace SmartLock.Domain.Accesses;

public static class AccessErrors
{
    public static Error SameRole = new(
        "Access.SameRole", "The role is already assigned.");
}
