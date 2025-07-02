using AutoMapper;
using SmartLock.Application.Features.Actions;
using SmartLock.Domain.Features.Devices.Actions;

namespace SmartLock.Api.Features.Actions;

public class ActionsMapperProfile : Profile
{
    public ActionsMapperProfile()
    {
        CreateMap<ActionModel, ActionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.RequestedOn, opt => opt.MapFrom(src => src.RequestedOn))
            .ForMember(dest => dest.ExecutedOn, opt => opt.MapFrom(src => src.ExecutedOn))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
    }
}
