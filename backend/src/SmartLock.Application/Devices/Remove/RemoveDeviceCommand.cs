using MediatR;

namespace SmartLock.Application.Devices.Remove;

public record RemoveDeviceCommand(Guid DeviceId) : IRequest;