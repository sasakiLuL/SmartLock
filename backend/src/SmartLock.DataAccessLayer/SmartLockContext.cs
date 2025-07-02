using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.Actions;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.Features.Users;
using System.Reflection;

namespace SmartLock.DataAccessLayer;

public class SmartLockContext : DbContext
{
    public SmartLockContext() : base() { }

    public SmartLockContext(DbContextOptions<SmartLockContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; private set; }

    public DbSet<DeviceModel> Devices { get; private set; }

    public DbSet<ActionModel> Actions { get; private set; }

    public DbSet<StateModel> States { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<INotification>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
