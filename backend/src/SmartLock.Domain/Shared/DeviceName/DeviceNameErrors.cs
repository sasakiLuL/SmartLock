using SmartLock.Domain.Core;

namespace SmartLock.Domain.Shared.DeviceName;

public static class DeviceNameErrors
{
    public readonly static Error TooLong = new Error(
        "DeviceName.TooLong",
        "The device name is too long.");

    public readonly static Error InvalidFormat = new Error(
        "DeviceName.InvalidFormat",
        "The device name contains unacceptable symbols.");
}
