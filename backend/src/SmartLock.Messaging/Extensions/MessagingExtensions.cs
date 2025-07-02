using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SmartLock.Messaging.Extensions;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, Action<MessagingBuilder> configure)
    {
        var builder = new MessagingBuilder(services);
        configure(builder);
        builder.Build();
        return services;
    }
    public static async Task UseMessagingAsync(this IApplicationBuilder app, CancellationToken cancellationToken = default)
    {
        var messagingService = app.ApplicationServices.GetRequiredService<MessagingService>();

        await messagingService.ConnectAsync(cancellationToken);
    }
}
