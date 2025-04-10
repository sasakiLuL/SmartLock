using SmartLock.Domain.Core;

namespace SmartLock.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid id) => new Error(
        "User.NotFound", 
        $"User with identifier: {id} was not found.");

    public static Error NonUniqueEmail(string email) => new Error(
        "User.EmailDuplication",
        $"User with email address: {email} is already exist.");

    public static Error NonUniqueUsername(string username) => new Error(
        "User.EmailDuplication",
        $"User with username: {username} is already exist.");

    public static Error InvalidUserCredentials => new Error(
        "User.InvalidUserCredentials",
        "Provided user credentials are invalid.");
}
