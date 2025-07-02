using AutoMapper;
using SmartLock.Api.Features.Devices.Add;
using SmartLock.Application.Features.Actions;
using SmartLock.Application.Features.Devices;
using SmartLock.Application.Features.Devices.Add;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.ValueObjects.DeviceNames;

namespace SmartLock.Api.Features.Devices;

public class DeviceMapperProfile : Profile
{
    public DeviceMapperProfile()
    {
        CreateMap<AddDeviceRequest, AddDeviceCommand>()
            .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName))
            .ForMember(dest => dest.HardwareId, opt => opt.MapFrom(src => src.HardwareId));

        CreateMap<DeviceModel, DeviceResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.HardwareId, opt => opt.MapFrom(src => src.HardwareId))
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName))
            .ForMember(dest => dest.RegisteredOnUtc, opt => opt.MapFrom(src => src.RegisteredOnUtc))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));

        CreateMap<ActionModel, ActionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RequestedOn, opt => opt.MapFrom(src => src.RequestedOn))
            .ForMember(dest => dest.ExecutedOn, opt => opt.MapFrom(src => src.ExecutedOn));
    
        CreateMap<StateModel, StateResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Locked, opt => opt.MapFrom(src => src.Locked))
            .ForMember(dest => dest.LastUpdatedOnUtc, opt => opt.MapFrom(src => src.LastUpdatedOnUtc));

        CreateMap<DeviceName, string>()
            .ConvertUsing(src => src.Value);
    }
}
