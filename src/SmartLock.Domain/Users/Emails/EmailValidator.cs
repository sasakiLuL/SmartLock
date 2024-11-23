using SmartLock.Core.Results;
using FluentValidation;
using SmartLock.Domain.Users.Emails;

namespace BergerDb.Domain.Users.Emails;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator() : base()
    {
        RuleFor(p => p.Value)
            .MaximumLength(Email.MaximumLength)
                .WithError(EmailErrors.TooLong)
            .Matches(Email.FormatString)
                .WithError(EmailErrors.InvalidFormat);
    }
}
