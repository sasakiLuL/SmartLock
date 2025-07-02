namespace SmartLock.Api.Features.Devices.Add;

public record AddDeviceRequest(
    Guid HardwareId, 
    string DeviceName);
