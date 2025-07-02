using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLock.Domain.Features.Users;

namespace SmartLock.DataAccessLayer.Features.Users;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.IdentityProviderId)
            .IsRequired();

        builder.ComplexProperty(
            x => x.Email, 
            builder =>
            {
                builder.Property(x => x.Value)
                    .IsRequired();
            });

        builder.ComplexProperty(
            x => x.Username,
            builder =>
            {
                builder.Property(x => x.Value)
                    .IsRequired();
            });
    }
}
