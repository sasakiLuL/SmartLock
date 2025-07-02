using MediatR;

namespace SmartLock.Application.Features.Devices.Activate;

public record ActivateRequestCommand(Guid DeviceId) : IRequest;
