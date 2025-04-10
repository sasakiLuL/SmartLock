using MediatR;

namespace SmartLock.Application.Devices.ActivationRequest;

public record ActivationRequestCommand(Guid HardwareId) : IRequest<Guid>;
