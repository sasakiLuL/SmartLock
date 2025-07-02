using SmartLock.Domain.Exceptions;

namespace SmartLock.Domain.Features.Devices;

public static class DeviceErrors
{
    public static Error NotFound(Guid id) => new(
        "Device.NotFound",
        $"The device with identifier: {id} was not found.");

    public static Error Activated(Guid id) => new(
        "Device.Activated",
        $"The device with identifier: {id} is already activated.");

    public static Error Unactivated(Guid id) => new(
        "Device.Unactivated",
        $"The device with identifier: {id} is not activated.");

    public static Error NoPendingActions(Guid id) => new(
        "Device.NoPendingActions",
        $"The device with identifier: {id} has no pending actions.");
}
