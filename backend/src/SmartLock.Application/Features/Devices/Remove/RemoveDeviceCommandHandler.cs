using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Devices.Remove;

public class RemoveDeviceCommandHandler(
    IUserCredentialsProvider userCredentialsProvider,
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveDeviceCommand>
{
    public async Task Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
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

        if (device.Model.State.Status == DeviceStatus.Activated)
        {
            throw new BadRequestException(DeviceErrors.Activated(request.DeviceId));
        }

        deviceRepository.Delete(device);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
