using FluentValidation;
using SmartLock.Domain.Core;

namespace SmartLock.Domain.Shared.EmailAddresses;

public record EmailAddress : ValueObject<string>
{
    internal EmailAddress(string value) : base(value) { }

    public static EmailAddress CreateAndThrow(string value)
    {
        var emailAddress = new EmailAddress(value);

        new EmailAddressValidator().ValidateAndThrow(emailAddress);

        return emailAddress;
    }
}
