using BergerDb.Domain.Users.Emails;
using SmartLock.Core.Entities;
using SmartLock.Core.Results;

namespace SmartLock.Domain.Users.Emails;

public record Email : ValueObject
{
    public const int MaximumLength = 256;

    public const string FormatString = @"^[\w-\.]+@([\w-]+\.)+[\w-]{1,4}$";

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        return Validate(new EmailValidator(), new Email(value));
    }
}
