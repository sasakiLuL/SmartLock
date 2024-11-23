using SmartLock.Core.Errors;

namespace SmartLock.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid id) => new("User.NotFound", $"User with id = {id} was not found.");

    public static readonly Error InvalidUserCredentials = new(
        "User.InvalidUserCredentials",
        "Invalid user credentials.");

    public static readonly Error InvalidPermissions = new(
        "User.InvalidPermissions",
        "The current user does not have the permissions to perform that operation.");
}
