using AutoMapper;
using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Devices.GetAll;

public class GetAllDevicesQueryHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IMapper mapper) : IRequestHandler<GetAllDevicesQuery, List<DeviceResponse>>
{
    public async Task<List<DeviceResponse>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var devices = await deviceRepository.ReadAllByOwnerIdAsync(user.Id, cancellationToken);

        return devices.Select(mapper.Map<DeviceResponse>).ToList();
    }
}
