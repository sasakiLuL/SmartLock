using SmartLock.Domain.Core;

namespace SmartLock.Domain.Shared.EmailAddresses;

public static class EmailAddressErrors
{
    public static Error InvalidFormat = new Error(
        "Email.InvalidFormat", "The email contains unacceptable symbols.");
}
