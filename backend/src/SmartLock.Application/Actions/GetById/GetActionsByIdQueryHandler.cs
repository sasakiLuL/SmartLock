using AutoMapper;
using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Actions.GetById;

public class GetActionsByIdQueryHandler(
    IUserRepository userRepository,
    IActionRepository actionRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetActionsByIdQuery, ActionResponse>
{
    public async Task<ActionResponse> Handle(GetActionsByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var action = await actionRepository.ReadByIdAsync(
            request.ActionId,
            cancellationToken) ?? throw new NotFoundException(ActionErrors.NotFound(request.ActionId));

        if (action.UserId != user.Id)
        {
            throw new NotFoundException(ActionErrors.NotFound(request.ActionId));
        }

        return mapper.Map<ActionResponse>(action);
    }
}
