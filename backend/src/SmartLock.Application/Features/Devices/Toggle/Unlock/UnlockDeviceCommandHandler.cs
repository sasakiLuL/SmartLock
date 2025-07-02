using MediatR;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;

namespace SmartLock.Application.Features.Devices.Toggle.Unlock;

public class UnlockDeviceCommandHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<UnlockDeviceCommand>
{
    public async Task Handle(UnlockDeviceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var device = await deviceRepository.ReadByIdAsync(request.DeviceId, cancellationToken) ??
            throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));

        if (device.Model.OwnerId != user.Model.Id)
        {
            throw new NotFoundException(DeviceErrors.NotFound(request.DeviceId));
        }

        device.Lock(false);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
