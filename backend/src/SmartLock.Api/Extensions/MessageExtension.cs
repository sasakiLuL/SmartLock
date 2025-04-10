using SmartLock.Messaging.Abstractions;

namespace SmartLock.Api.Extensions;

public static class MessageExtension
{
    public static void UseMessaging(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

        IDeviceMessageService service = 
            serviceScope.ServiceProvider.GetRequiredService<IDeviceMessageService>();

        service.ConnectAsync().GetAwaiter().GetResult();

        IDeviceMessageConsumer consumer =
            serviceScope.ServiceProvider.GetRequiredService<IDeviceMessageConsumer>();

        consumer.StartConsumingAsync().GetAwaiter().GetResult();
    }
}
