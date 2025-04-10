using SmartLock.Domain.Devices;

namespace SmartLock.Application.Devices;

public record DeviceResponse(
    Guid Id,
    Guid HardwareId,
    string? DeviceName,
    DeviceStatus DeviceStatus,
    DateTime RegisteredOnUtc);
