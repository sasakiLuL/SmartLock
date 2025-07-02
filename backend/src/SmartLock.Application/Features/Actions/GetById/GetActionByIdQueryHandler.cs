using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Actions.GetById;

public class GetActionByIdQueryHandler(
    IUserRepository userRepository,
    IReadModelService readModelService,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetActionByIdQuery, ActionResponse>
{
    public async Task<ActionResponse> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var action = await readModelService.Query<ActionModel>()
            .FirstOrDefaultAsync(x => x.Id == request.ActionId && x.UserId == user.Model.Id, cancellationToken) 
                ?? throw new NotFoundException(ActionErrors.NotFound(request.ActionId));

        return mapper.Map<ActionResponse>(action);
    }
}
