using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Users;

namespace SmartLock.Application.Features.Devices.Deactivate;

public class DeactivateRequestCommandHandler(
    IUserCredentialsProvider userCredentialsProvider,
    IUserRepository userRepository,
    IDeviceRepository deviceRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeactivateRequestCommand>
{
    public async Task Handle(DeactivateRequestCommand request, CancellationToken cancellationToken)
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

        device.Deactivate();

        await unitOfWork.CommitAsync(cancellationToken);
    }
}
