namespace SmartLock.Application.Abstractions;

public interface IUserCredentialsProvider
{
    public Guid UserId { get; }
}
