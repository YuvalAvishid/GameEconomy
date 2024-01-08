using System.Reflection;
using GameCommon.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameCommon.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
    {        
        services.AddMassTransit(configure => 
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            
            configure.UsingRabbitMq((context,cfg) =>
            {
                var configurations = context.GetService<IConfiguration>();
                var rabbitMqSettings = configurations.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>() ?? new RabbitMQSettings();
                var serviceSettings = configurations.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new ServiceSettings();
                cfg.Host(rabbitMqSettings.Host);
                cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
            });
        });

        return services;
    }
}
