using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.DataAccessLayer.Devices;

public class DeviceConfiguration : IEntityTypeConfiguration<DeviceModel>
{
    public void Configure(EntityTypeBuilder<DeviceModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.HardwareId)
            .IsRequired();

        builder.Property(x => x.RegisteredOnUtc)
            .IsRequired();

        builder.Property(x => x.DeviceName);

        builder.Property(x => x.DeviceStatus)
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }
}
