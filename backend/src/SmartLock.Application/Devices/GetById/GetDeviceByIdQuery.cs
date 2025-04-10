using MediatR;

namespace SmartLock.Application.Devices.GetById;

public record GetDeviceByIdQuery(Guid DeviceId) : IRequest<DeviceResponse>;
