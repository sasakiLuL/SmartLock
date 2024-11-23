using SmartLock.Core.Entities;
using SmartLock.Core.Results;

namespace SmartLock.Domain.Locks.MacAddresses;

public record MacAddress : ValueObject
{
    private MacAddress(byte[] bytes)
    {
        RawValue = bytes;
    }

    public string Value { get => $"{RawValue[0]:x}:{RawValue[1]:x}"; }

    public byte[] RawValue { get; }

    public static Result<MacAddress> Create(byte[] bytes)
    {
        return Validate(new MacAddressValidator(), new MacAddress(bytes));
    }
}
