using SmartLock.Core.Entities;
using SmartLock.Core.Results;
using SmartLock.Domain.Locks.LockName;
using System.Text.RegularExpressions;

namespace SmartLock.Domain.Locks.LockNames;

public record LockName : ValueObject
{
    public static readonly int MaximumLength = 255;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$");

    public string Value { get; }

    private LockName(string value)
    {
        Value = value;
    }

    public static Result<LockName> Create(string value)
    {
        return Validate(new LockNameValidator(), new LockName(value));
    }
}
