using MediatR;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;

namespace SmartLock.Application.Features.Devices.Toggle.Lock;

public class LockDeviceCommandHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<LockDeviceCommand>
{
    public async Task Handle(LockDeviceCommand request, CancellationToken cancellationToken)
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

        device.Lock(true);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
