using FluentValidation;
using SmartLock.Core.Results;

namespace SmartLock.Domain.Locks.LockNames;

public class LockNameValidator : AbstractValidator<LockName>
{
    public LockNameValidator() : base()
    { 
        RuleFor(v => v.Value)
            .MaximumLength(LockName.MaximumLength)
                .WithError(LockNameErrors.TooLong)
            .Matches(LockName.AllowedSymbolsPattern)
                .WithError(LockNameErrors.InvalidFormat);
    }
}
