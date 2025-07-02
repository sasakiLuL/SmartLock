using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Actions.GetPendingAction;

public class GetPendingActionQueryHandler(
    IUserRepository userRepository,
    IReadModelService readModelService,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetPendingActionQuery, ActionResponse>
{
    public async Task<ActionResponse> Handle(GetPendingActionQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var action = await readModelService.Query<ActionModel>()
            .FirstOrDefaultAsync(x => 
                    x.DeviceId == request.DeviceId && 
                    x.UserId == user.Model.Id &&
                    x.Status == ActionStatus.Pending, 
                cancellationToken)
                ?? throw new NotFoundException(DeviceErrors.NoPendingActions(request.DeviceId));

        return mapper.Map<ActionResponse>(action);
    }
}
