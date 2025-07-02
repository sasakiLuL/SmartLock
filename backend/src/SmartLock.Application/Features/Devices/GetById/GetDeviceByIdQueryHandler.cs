using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Devices.GetById;

public class GetDeviceByIdQueryHandler(
    IUserRepository userRepository,
    IReadModelService readModelService,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetDeviceByIdQuery, DeviceResponse>
{
    public async Task<DeviceResponse> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var device = await readModelService
            .Query<DeviceModel>()
            .Include(x => x.Actions)
            .Include(x => x.State)
            .FirstOrDefaultAsync(x => x.Id == request.DeviceId && x.OwnerId == user.Model.Id, cancellationToken) 
                ?? throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));

        return mapper.Map<DeviceResponse>(device);
    }
}
