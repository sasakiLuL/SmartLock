using FluentValidation;
using SmartLock.Domain.Extensions;

namespace SmartLock.Domain.ValueObjects.DeviceNames;

public class DeviceNameValidator : AbstractValidator<DeviceName>
{
    public DeviceNameValidator()
    {
        RuleFor(x => x.Value)
            .MaximumLength(DeviceName.MaximumLenght)
                .WithError(DeviceNameErrors.TooLong)
            .Matches(DeviceName.FormatString)
                .WithError(DeviceNameErrors.InvalidFormat);
    }
}
