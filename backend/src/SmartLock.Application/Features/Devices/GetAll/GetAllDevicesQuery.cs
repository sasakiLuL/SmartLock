using MediatR;

namespace SmartLock.Application.Features.Devices.GetAll;

public record GetAllDevicesQuery : IRequest<List<DeviceResponse>>;
