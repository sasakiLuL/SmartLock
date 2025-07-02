using FluentValidation;
using SmartLock.Domain.Extensions;

namespace SmartLock.Domain.ValueObjects.Usernames;

public class UsernameValidator : AbstractValidator<Username>
{
    public UsernameValidator()
    {
        RuleFor(x => x.Value)
            .MaximumLength(Username.MaximumLenght)
                .WithError(UsernameErrors.TooLong)
            .Matches(Username.FormatString)
                .WithError(UsernameErrors.InvalidFormat);
    }
}
