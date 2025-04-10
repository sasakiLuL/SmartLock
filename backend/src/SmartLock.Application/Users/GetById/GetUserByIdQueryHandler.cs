using AutoMapper;
using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Users.GetById;

public class GetUserByIdQueryHandler(
    IUserRepository userRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdAsync(
            request.Id,
            cancellationToken) ?? throw new NotFoundException(UserErrors.NotFound(request.Id));

        if (user.IdentityProviderId != userCredentialsProvider.UserId)
        {
            throw new ForbiddenException();
        }

        return mapper.Map<UserResponse>(user);
    }
}
