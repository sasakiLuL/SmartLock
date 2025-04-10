using MediatR;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;
using SmartLock.Domain.Core.Exceptions;
using Action = SmartLock.Domain.Actions.Action;

namespace SmartLock.Application.Devices.Open;

public class OpenDeviceCommandHandler(
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IActionRepository actionRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IDeviceMessagePublisher deviceMessagePublisher,
    IUnitOfWork unitOfWork) : IRequestHandler<OpenDeviceCommand>
{
    public async Task Handle(OpenDeviceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        var device = await deviceRepository.ReadByIdAsync(
            request.DeviceId,
            cancellationToken) ?? throw new BadRequestException(DeviceErrors.NotFound(request.DeviceId));

        if (device.DeviceStatus != DeviceStatus.Activated)
        {
            throw new BadRequestException(DeviceErrors.IsNotActivated(request.DeviceId));
        }

        var message = new DeviceActionMessage(device.HardwareId, CommandType.Open);

        await deviceMessagePublisher.PublishAsync(message, MessagePolicy.Actions, cancellationToken);

        //var action = Action.Create(user.Id, device.Id, CommandType.Open, DateTime.UtcNow);

        //await actionRepository.CreateAsync(action, cancellationToken);

        //await unitOfWork.CommitAsync(cancellationToken);
    }
}
