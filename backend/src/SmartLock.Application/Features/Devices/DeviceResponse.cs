namespace SmartLock.Application.Features.Devices;

public record DeviceResponse(
    Guid Id,
    Guid HardwareId,
    Guid OwnerId,
    string DeviceName,
    StateResponse State,
    DateTime RegisteredOnUtc);
