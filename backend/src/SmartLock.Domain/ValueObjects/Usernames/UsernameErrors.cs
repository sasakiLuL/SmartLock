using SmartLock.Domain.Exceptions;

namespace SmartLock.Domain.ValueObjects.Usernames;

public static class UsernameErrors
{
    public readonly static Error TooLong = new Error(
        "Username.TooLong",
        "The username is too long.");

    public readonly static Error InvalidFormat = new Error(
        "Username.InvalidFormat",
        "The username contains unacceptable symbols.");
}
