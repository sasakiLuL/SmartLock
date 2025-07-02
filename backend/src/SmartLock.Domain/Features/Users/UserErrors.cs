using SmartLock.Domain.Exceptions;

namespace SmartLock.Domain.Features.Users;

public static class UserErrors
{
    public static Error NotFound(Guid id) => new(
        "User.NotFound",
        $"User with identifier: {id} was not found.");

    public static Error NonUniqueEmail(string email) => new(
        "User.EmailDuplication",
        $"User with email address: {email} is already exist.");

    public static Error NonUniqueUsername(string username) => new(
        "User.EmailDuplication",
        $"User with username: {username} is already exist.");

    public static Error InvalidUserCredentials => new(
        "User.InvalidUserCredentials",
        "Provided user credentials are invalid.");
}
