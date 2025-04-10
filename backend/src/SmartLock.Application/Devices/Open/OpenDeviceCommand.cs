using MediatR;

namespace SmartLock.Application.Devices.Open;

public record OpenDeviceCommand(
    Guid DeviceId) : IRequest;
