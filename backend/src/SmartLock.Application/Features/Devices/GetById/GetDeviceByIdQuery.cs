using MediatR;

namespace SmartLock.Application.Features.Devices.GetById;

public record GetDeviceByIdQuery(Guid DeviceId) : IRequest<DeviceResponse>;
