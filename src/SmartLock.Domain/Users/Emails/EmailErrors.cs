using SmartLock.Core.Errors;

namespace BergerDb.Domain.Users.Emails;

public static class EmailErrors
{
    public static readonly Error TooLong = new(
        "Email.TooLong", "The email is too long.");

    public static readonly Error InvalidFormat = new(
        "Email.InvalidFormat", "The email contains unacceptable symbols.");
}
