using MediatR;
using SmartLock.Application.Abstractions;
using SmartLock.Domain.Core.Exceptions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Shared.EmailAddresses;
using SmartLock.Domain.Shared.Usernames;
using SmartLock.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace SmartLock.Application.Users.Register;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserCredentialsProvider userCredentialsProvider,
    IIdentityService identityProviderService) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand notification, CancellationToken cancellationToken)
    {
        var isExistResult = await identityProviderService.IsExistsAsync(
            userCredentialsProvider.UserId, 
            cancellationToken);

        if (!isExistResult)
        {
            throw new BadRequestException(UserErrors.InvalidUserCredentials);
        }

        var email = EmailAddress.CreateAndThrow(notification.Email);

        if (!await userRepository.IsEmailUniqueAsync(email.Value, cancellationToken))
        {
            throw new BadRequestException(
                BadRequestException.ValidationErrorMessage,
                UserErrors.NonUniqueEmail(email.Value));
        }

        var userName = Username.CreateAndThrow(notification.UserName);

        if (!await userRepository.IsUsernameUniqueAsync(userName.Value, cancellationToken))
        {
            throw new BadRequestException(
                BadRequestException.ValidationErrorMessage,
                UserErrors.NonUniqueUsername(userName.Value));
        }

        var id = Guid.NewGuid();

        var user = User.Create(
            id,
            userCredentialsProvider.UserId,
            email,
            userName);

        await userRepository.CreateAsync(user, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return id;
    }
}
