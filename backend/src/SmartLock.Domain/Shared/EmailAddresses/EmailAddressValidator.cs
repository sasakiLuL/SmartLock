using FluentValidation;
using SmartLock.Domain.Core.Extensions;

namespace SmartLock.Domain.Shared.EmailAddresses;

public class EmailAddressValidator : AbstractValidator<EmailAddress>
{
    public EmailAddressValidator()
    {
        RuleFor(x => x.Value)
            .EmailAddress()
                .WithError(EmailAddressErrors.InvalidFormat);
    }
}
