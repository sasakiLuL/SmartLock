using AutoMapper;
using SmartLock.Api.Features.Users.Register;
using SmartLock.Application.Features.Users;
using SmartLock.Application.Features.Users.Register;
using SmartLock.Domain.Features.Users;
using SmartLock.Domain.ValueObjects.EmailAddresses;
using SmartLock.Domain.ValueObjects.Usernames;

namespace SmartLock.Api.Features.Users;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForMember(x => x.Email, x => x.MapFrom(x => x.Email))
            .ForMember(x => x.UserName, x => x.MapFrom(x => x.Username));

        CreateMap<UserModel, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(opt => opt.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

        CreateMap<EmailAddress, string>()
            .ConvertUsing(src => src.Value);

        CreateMap<Username, string>()
            .ConvertUsing(src => src.Value);
    }
}
