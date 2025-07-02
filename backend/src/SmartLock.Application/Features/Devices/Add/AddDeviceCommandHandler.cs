using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;
using SmartLock.Domain.ValueObjects.DeviceNames;

namespace SmartLock.Application.Features.Devices.Add;

public class AddDeviceCommandHandler(
    IUserCredentialsProvider userCredentialsProvider,
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddDeviceCommand, Guid>
{
    public async Task<Guid> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        if (await deviceRepository.IsAlreadyExistByHardwareId(request.HardwareId, cancellationToken))
        {
            throw new BadRequestException(DeviceErrors.Activated(request.HardwareId));
        }

        var device = Device.Create(
            Guid.NewGuid(),
            DeviceName.CreateAndThrow(request.DeviceName),
            request.HardwareId,
            user.Model.Id);

        await deviceRepository.CreateAsync(device, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return device.Model.Id;
    }
}
