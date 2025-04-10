using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Actions;
using SmartLock.Domain.Devices;
using SmartLock.Domain.Users;

namespace SmartLock.DataAccessLayer.Actions;

public class ActionConfiguration : IEntityTypeConfiguration<ActionModel>
{
    public void Configure(EntityTypeBuilder<ActionModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.OccurredOn)
            .IsRequired();

        builder.Property(x => x.CommandType)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.DeviceId)
            .IsRequired();

        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne<DeviceModel>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId);
    }
}
