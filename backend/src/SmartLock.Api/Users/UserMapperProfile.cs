using AutoMapper;
using SmartLock.Api.Users.Register;
using SmartLock.Application.Users;
using SmartLock.Application.Users.Register;
using SmartLock.Domain.Shared.EmailAddresses;
using SmartLock.Domain.Shared.Usernames;
using SmartLock.Domain.Users;

namespace SmartLock.Api.Users;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForMember(x => x.Email, x => x.MapFrom(x => x.Email))
            .ForMember(x => x.UserName, x => x.MapFrom(x => x.Username));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(opt => opt.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

        CreateMap<EmailAddress, string>()
            .ConvertUsing(src => src.Value);

        CreateMap<Username, string>()
            .ConvertUsing(src => src.Value);
    }
}
