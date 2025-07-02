using FluentValidation;
using SmartLock.Domain.Extensions;

namespace SmartLock.Domain.ValueObjects.EmailAddresses;

public class EmailAddressValidator : AbstractValidator<EmailAddress>
{
    public EmailAddressValidator()
    {
        RuleFor(x => x.Value)
            .EmailAddress()
                .WithError(EmailAddressErrors.InvalidFormat);
    }
}
