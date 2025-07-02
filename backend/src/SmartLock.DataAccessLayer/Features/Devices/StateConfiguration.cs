using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Features.Devices.States;

namespace SmartLock.DataAccessLayer.Features.Devices;

public class StateConfiguration : IEntityTypeConfiguration<StateModel>
{
    public void Configure(EntityTypeBuilder<StateModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.DeviceId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.LastUpdatedOnUtc)
            .IsRequired(); ;

        builder.Property(x => x.Locked)
            .IsRequired();
    }
}
