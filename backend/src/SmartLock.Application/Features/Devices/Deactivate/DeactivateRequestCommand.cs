using MediatR;

namespace SmartLock.Application.Features.Devices.Deactivate;

public record DeactivateRequestCommand(
    Guid DeviceId) : IRequest;
