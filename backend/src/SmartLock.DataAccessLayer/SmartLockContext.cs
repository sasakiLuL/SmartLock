using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;
using System.Reflection;

namespace SmartLock.DataAccessLayer;

public class SmartLockContext : DbContext
{
    public SmartLockContext() : base() { }

    public SmartLockContext(DbContextOptions<SmartLockContext> options) : base(options) { }

    public DbSet<UserModel> UserModels { get; private set; }

    public DbSet<DeviceModel> DeviceModels { get; private set; }

    public DbSet<ActionModel> ActionModels { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
