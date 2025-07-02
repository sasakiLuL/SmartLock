using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Devices.Activate;

public class ActivateRequestCommandHandler(
    IDeviceRepository deviceRepository,
    IUserRepository userRepository,
    IUserCredentialsProvider userCredentialsProvider,
    IUnitOfWork unitOfWork) : IRequestHandler<ActivateRequestCommand>
{
    public async Task Handle(ActivateRequestCommand request, CancellationToken cancellationToken)
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

        device.Activate(user.Model.Username.Value);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
