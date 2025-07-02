using MediatR;

namespace SmartLock.Application.Features.Devices.Toggle.Unlock;

public record UnlockDeviceCommand(Guid DeviceId) : IRequest;
