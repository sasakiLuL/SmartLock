using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Application.Abstractions.Messages;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.Application.Devices.ActivationRequest;

public class ActivationRequestCommandHandler(
    IDeviceRepository deviceRepository,
    IUserRepository userRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IDeviceMessagePublisher deviceMessagePublisher,
    IUnitOfWork unitOfWork) : IRequestHandler<ActivationRequestCommand, Guid>
{
    public async Task<Guid> Handle(ActivationRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.ReadByIdentityProviderIdAsync(
            userCredentialsProvider.UserId,
            cancellationToken) ?? throw new ForbiddenException();

        if (await deviceRepository.IsAlreadyExist(request.HardwareId, cancellationToken))
        {
            throw new BadRequestException(DeviceErrors.AlreadyActivated(request.HardwareId));
        }

        var deviceId = Guid.NewGuid();

        var registeredTimeUtc = DateTime.UtcNow;

        var device = Device.Create(
            deviceId,
            request.HardwareId,
            null,
            DeviceStatus.Pending,
            registeredTimeUtc,
            user.Id);

        var message = new ActivationRequestMessage(
            request.HardwareId,
            user.Username.Value);

        await deviceRepository.CreateAsync(device, cancellationToken);

        await deviceMessagePublisher.PublishAsync(message, MessagePolicy.ActivationRequests, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return deviceId;
    }
}
