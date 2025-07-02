using MediatR;

namespace SmartLock.Application.Features.Devices.Add;

public record AddDeviceCommand(Guid HardwareId, string DeviceName) : IRequest<Guid>;
