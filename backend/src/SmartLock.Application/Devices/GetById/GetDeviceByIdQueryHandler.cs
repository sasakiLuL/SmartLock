using AutoMapper;
using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Devices.GetById;

public class GetDeviceByIdQueryHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetDeviceByIdQuery, DeviceResponse>
{
    public async Task<DeviceResponse> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var device = await deviceRepository.ReadByIdAsync(
            request.DeviceId, 
            cancellationToken) ?? throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));

        if (device.OwnerId != user.Id)
        {
            throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));
        }

        return mapper.Map<DeviceResponse>(device);
    }
}
