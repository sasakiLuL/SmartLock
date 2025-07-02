using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Features.Devices;
using SmartLock.Domain.Features.Devices.States;
using SmartLock.Domain.Features.Users;

namespace SmartLock.DataAccessLayer.Features.Devices;

public class DeviceConfiguration : IEntityTypeConfiguration<DeviceModel>
{
    public void Configure(EntityTypeBuilder<DeviceModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.HardwareId)
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.Property(x => x.RegisteredOnUtc)
            .IsRequired();

        builder.ComplexProperty(
            x => x.DeviceName, 
            builder =>
            {
                builder.Property(x => x.Value)
                    .IsRequired();
            });

        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(d => d.Actions)
            .WithOne()
            .HasForeignKey(a => a.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.State)
            .WithOne()
            .HasForeignKey<StateModel>(x => x.DeviceId);
    }
}
