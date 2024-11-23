using SmartLock.Core.Errors;

namespace SmartLock.Domain.Locks.MacAddresses;

public static class MacAddressErrors
{
    public static readonly Error InvalidValue = new(
        "MacAddress.InvalidValue", "The mac address value is invalid.");
}
