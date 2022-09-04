using App.Options;
using BotCommands.Commands;
using NServiceBus;
using Endpoint = NServiceBus.Endpoint;

namespace App.Config;

public static class NServiceBusConfig
{
    public static void AddNServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSession>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var configOptions = configuration.GetSection(NServiceBusConfigOptions.SectionName)
                .Get<NServiceBusConfigOptions>();
            var endpointConfiguration = new EndpointConfiguration(configOptions.ChatEndpoint);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(configOptions.TransportConnection);

            transport.UseDirectRoutingTopology(QueueType.Quorum);

            transport
                .Routing()
                .RouteToEndpoint(typeof(SendStockCommand), configOptions.SendStockCommandDestination);

            return Endpoint.Start(endpointConfiguration)
                .GetAwaiter()
                .GetResult();
        });
    }

    public static void UseNServiceBusInstance(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<IMessageSession>();
    }
}