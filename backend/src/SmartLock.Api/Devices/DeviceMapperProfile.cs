using AutoMapper;
using SmartLock.Application.Devices;
using SmartLock.Domain.Devices;

namespace SmartLock.Api.Devices;

public class DeviceMapperProfile : Profile
{
    public DeviceMapperProfile()
    {
        CreateMap<Device, DeviceResponse>()
            .ConstructUsing(src => new DeviceResponse(
                src.Id,
                src.HardwareId,
                src.DeviceName != null ? src.DeviceName.Value : null,
                src.DeviceStatus,
                src.RegisteredOnUtc));
    }
}
