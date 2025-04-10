using SmartLock.Domain.Core;

namespace SmartLock.Domain.Shared.Usernames;

public static class UsernameErrors
{
    public readonly static Error TooLong = new Error(
        "Username.TooLong", 
        "The username is too long.");

    public readonly static Error InvalidFormat = new Error(
        "Username.InvalidFormat", 
        "The username contains unacceptable symbols.");
}
