using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SmartLock.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
