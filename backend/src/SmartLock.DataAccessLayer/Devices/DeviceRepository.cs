using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.DataAccessLayer.Locks;

public class DeviceRepository : Repository<Device, DeviceModel>, IDeviceRepository
{
    public DeviceRepository(SmartLockContext smartLockContext) : base(smartLockContext)
    {
    }

    public async Task<bool> IsAlreadyExist(Guid hardwareId, CancellationToken cancellationToken = default)
    {
        return await _context.DeviceModels.AnyAsync(x => x.HardwareId == hardwareId);
    }

    public async Task<List<Device>> ReadAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _context.DeviceModels
            .Where(x => x.OwnerId == ownerId)
            .Select(x => CreateEntityFromModel(x))
            .ToListAsync(cancellationToken);
    }

    public async Task<Device?> ReadByHardwareIdAsync(Guid hardwareId, CancellationToken cancellationToken = default)
    {
        var deviceModel = await _context.DeviceModels.FirstOrDefaultAsync(x => x.HardwareId == hardwareId, cancellationToken);

        if (deviceModel is null)
        {
            return null;
        }

        return CreateEntityFromModel(deviceModel);
    }

    public async Task<Device?> ReadByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var deviceModel = await _context.DeviceModels.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (deviceModel is null)
        {
            return null;
        }

        return CreateEntityFromModel(deviceModel);
    }
}
