using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Features.Devices.Actions;

namespace SmartLock.DataAccessLayer.Features.Devices;

public class ActionConfiguration : IEntityTypeConfiguration<ActionModel>
{
    public void Configure(EntityTypeBuilder<ActionModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.DeviceId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.RequestedOn)
            .IsRequired();

        builder.Property(x => x.ExecutedOn);
    }
}
