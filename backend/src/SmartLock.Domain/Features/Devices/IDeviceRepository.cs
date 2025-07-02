using SmartLock.Domain.Interfaces;

namespace SmartLock.Domain.Features.Devices;

public interface IDeviceRepository : IRepository<Device, DeviceModel>
{
    Task<Device?> ReadByHardwareIdAsync(Guid hardwareId, CancellationToken cancellationToken = default);

    Task<Device?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> IsAlreadyExistByHardwareId(Guid hardwareId, CancellationToken cancellationToken = default);
}
