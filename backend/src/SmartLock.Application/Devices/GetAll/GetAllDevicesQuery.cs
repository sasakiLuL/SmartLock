using MediatR;

namespace SmartLock.Application.Devices.GetAll;

public record GetAllDevicesQuery : IRequest<List<DeviceResponse>>;
