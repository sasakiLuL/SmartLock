using SmartLock.Domain.Core;

namespace SmartLock.Domain.Devices;

public interface IDeviceRepository : IRepository<Device, DeviceModel>
{
    Task<List<Device>> ReadAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);

    Task<Device?> ReadByHardwareIdAsync(Guid hardwareId, CancellationToken cancellationToken = default);

    Task<Device?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> IsAlreadyExist(Guid hardwareId, CancellationToken cancellationToken = default);
}
