using AutoMapper;
using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Actions.GetAll;

public class GetAllActionsQueryHandler(
    IUserRepository userRepository,
    IActionRepository actionRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetAllActionsQuery, List<ActionResponse>>
{
    public async Task<List<ActionResponse>> Handle(GetAllActionsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var actions = await actionRepository.ReadAllByUserIdAsync(user.Id, cancellationToken);

        return actions.Select(mapper.Map<ActionResponse>).ToList();
    }
}
