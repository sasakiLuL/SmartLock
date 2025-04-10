using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Application.Devices.Deactivate;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Devices.Remove;

public class RemoveDeviceCommandHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork,
    IUserCredentialsProvider userCredentialsProvider,
    IDeviceMessagePublisher deviceMessagePublisher) : IRequestHandler<RemoveDeviceCommand>
{
    public async Task Handle(RemoveDeviceCommand request, CancellationToken cancellationToken)
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

        var deactivationMessage = new DeviceActionMessage(device.HardwareId, Domain.Actions.CommandType.Deactivate);

        await deviceMessagePublisher.PublishAsync(deactivationMessage, MessagePolicy.Actions, cancellationToken);

        deviceRepository.Delete(device);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
