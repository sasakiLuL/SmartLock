using AutoMapper;
using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace SmartLock.Application.Features.Devices.GetAll;

public class GetAllDevicesQueryHandler(
   IUserRepository userRepository,
   IReadModelService readModelService,
   IUserCredentialsProvider userCredentialsProvider,
   IMapper mapper) : IRequestHandler<GetAllDevicesQuery, List<DeviceResponse>>
{
    public async Task<List<DeviceResponse>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var devices = await readModelService
            .Query<DeviceModel>()
            .Where(x => x.OwnerId == user.Model.Id)
            .Include(x => x.Actions)
            .Include(x => x.State)
            .ToListAsync(cancellationToken);

        return devices.Select(mapper.Map<DeviceResponse>).ToList();
    }
}
