using SmartLock.Domain.Exceptions;

namespace SmartLock.Domain.ValueObjects.EmailAddresses;

public static class EmailAddressErrors
{
    public static Error InvalidFormat = new Error(
        "Email.InvalidFormat", "The email contains unacceptable symbols.");
}
