using MediatR;

namespace SmartLock.Application.Features.Devices.Remove;

public record RemoveDeviceCommand(Guid DeviceId) : IRequest;
