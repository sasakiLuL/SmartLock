using SmartLock.Domain.Core;

namespace SmartLock.Domain.Devices;

public static class DeviceErrors
{
    public static Error NotFound(Guid id) => new Error(
        "Device.NotFound",
        $"The device with identifier: {id} was not found.");

    public static Error AlreadyActivated(Guid id) => new Error(
        "Device.AlreadyActivated",
        $"The device with identifier: {id} is already activated.");

    public static Error IsNotActivated(Guid id) => new Error(
        "Device.IsNotActivated",
        $"The device with identifier: {id} is not activated.");
}
