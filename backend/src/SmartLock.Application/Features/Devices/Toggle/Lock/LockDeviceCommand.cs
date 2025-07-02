using MediatR;

namespace SmartLock.Application.Features.Devices.Toggle.Lock;

public record LockDeviceCommand(Guid DeviceId) : IRequest;
