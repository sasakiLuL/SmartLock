using FluentValidation;
using SmartLock.Core.Results;

namespace SmartLock.Domain.Locks.MacAddresses;

public class MacAddressValidator : AbstractValidator<MacAddress>
{
    public MacAddressValidator() : base()
    {
        RuleFor(p => p.RawValue)
            .Must(v => v.Count() != 6)
                .WithError(MacAddressErrors.InvalidValue);
    }
}
