using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Users.GetById;

public class GetUserByIdQueryHandler(
    IReadModelService readModelService,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await readModelService
            .Query<UserModel>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) 
            ?? throw new NotFoundException(UserErrors.NotFound(request.Id));

        if (user.IdentityProviderId != userCredentialsProvider.UserId)
        {
            throw new ForbiddenException();
        }

        return mapper.Map<UserResponse>(user);
    }
}
