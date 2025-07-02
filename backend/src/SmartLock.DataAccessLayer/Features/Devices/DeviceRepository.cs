using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Features.Devices;

namespace SmartLock.DataAccessLayer.Features.Devices;

public class DeviceRepository(SmartLockContext smartLockContext) 
    : Repository<Device, DeviceModel>(smartLockContext), IDeviceRepository
{
    public async Task<bool> IsAlreadyExistByHardwareId(Guid hardwareId, CancellationToken cancellationToken = default)
    {
        return await _context.Devices
            .AnyAsync(x => x.HardwareId == hardwareId, cancellationToken);
    }

    public async Task<Device?> ReadByHardwareIdAsync(Guid hardwareId, CancellationToken cancellationToken = default)
    {
        var deviceModel = await _context.Devices
            .Include(x => x.Actions)
            .Include(x => x.State)
            .FirstOrDefaultAsync(x => x.HardwareId == hardwareId, cancellationToken);

        return deviceModel is not null ? new Device(deviceModel) : null;
    }

    public async Task<Device?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var deviceModel = await _context.Devices
            .Include(x => x.Actions)
            .Include(x => x.State)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        return deviceModel is not null ? new Device(deviceModel) : null;
    }
}
