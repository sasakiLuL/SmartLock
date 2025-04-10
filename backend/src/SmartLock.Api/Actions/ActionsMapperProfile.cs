using AutoMapper;
using SmartLock.Application.Actions;
using Action = SmartLock.Domain.Actions.Action;

namespace SmartLock.Api.Actions;

public class ActionsMapperProfile : Profile
{
    public ActionsMapperProfile()
    {
        CreateMap<Action, ActionResponse>()
            .ConstructUsing(src => new ActionResponse(
                src.Id,
                src.CommandType,
                src.OccuredOn));
    }
}
