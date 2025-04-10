using Microsoft.EntityFrameworkCore;
using SmartLock.DataAccessLayer;

namespace SmartLock.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        using SmartLockContext context =
            serviceScope.ServiceProvider.GetRequiredService<SmartLockContext>();

        context.Database.Migrate();
    }

    public static void DropDatabase(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        using SmartLockContext context =
            serviceScope.ServiceProvider.GetRequiredService<SmartLockContext>();

        context.Database.EnsureDeleted();
    }
}
