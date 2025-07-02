using MediatR;
using SmartLock.Application.Interfaces;
using SmartLock.Domain.Exceptions;
using SmartLock.Domain.Features.Users;
using SmartLock.Domain.ValueObjects.EmailAddresses;
using SmartLock.Domain.ValueObjects.Usernames;

namespace SmartLock.Application.Features.Users.Register;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserCredentialsProvider userCredentialsProvider,
    IIdentityService identityProviderService) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand notification, CancellationToken cancellationToken)
    {
        if (!await identityProviderService.IsExistsAsync(
                userCredentialsProvider.UserId, cancellationToken))
        {
            throw new BadRequestException(UserErrors.InvalidUserCredentials);
        }

        var email = EmailAddress.CreateAndThrow(notification.Email);

        if (!await userRepository.IsEmailUniqueAsync(email, cancellationToken))
        {
            throw new BadRequestException(
                BadRequestException.ValidationErrorMessage,
                UserErrors.NonUniqueEmail(email.Value));
        }

        var userName = Username.CreateAndThrow(notification.UserName);

        if (!await userRepository.IsUsernameUniqueAsync(userName, cancellationToken))
        {
            throw new BadRequestException(
                BadRequestException.ValidationErrorMessage,
                UserErrors.NonUniqueUsername(userName.Value));
        }

        var user = UserModel.Create(
            userCredentialsProvider.UserId,
            email,
            userName);

        await userRepository.CreateAsync(new User(user), cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return user.Id;
    }
}
