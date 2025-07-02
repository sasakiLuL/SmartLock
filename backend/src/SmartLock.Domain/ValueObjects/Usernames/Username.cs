using FluentValidation;

namespace SmartLock.Domain.ValueObjects.Usernames;

public record Username : ValueObject<string>
{
    public const int MaximumLenght = 100;

    public const string FormatString = @"^[a-zA-Z -!|]*";

    internal Username(string value) : base(value) { }

    public static Username CreateAndThrow(string value)
    {
        var userName = new Username(value);

        new UsernameValidator().ValidateAndThrow(userName);

        return userName;
    }
}
