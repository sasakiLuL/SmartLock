using FluentValidation;
using SmartLock.Domain.Core.Extensions;

namespace SmartLock.Domain.Shared.DeviceName;

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
