using FluentValidation;
using SmartLock.Domain.Core.Extensions;

namespace SmartLock.Domain.Shared.Usernames;

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
