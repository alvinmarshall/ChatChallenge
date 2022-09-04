using Bot.Options;
using BotCommands.Commands;
using NServiceBus;

namespace Bot.Config;

public static class NServiceBusConfig
{
    public static void AddNServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSession>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var configOptions = configuration.GetSection(NServiceBusConfigOptions.SectionName)
                .Get<NServiceBusConfigOptions>();
            var endpointConfiguration = new EndpointConfiguration(configOptions.ChatBotEndpoint);
            endpointConfiguration.EnableInstallers();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString(configOptions.TransportConnection);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            transport.UseDirectRoutingTopology(QueueType.Quorum);

            transport
                .Routing()
                .RouteToEndpoint(typeof(GetStockCommand), configOptions.GetStockCommandDestination);
            return NServiceBus.Endpoint.Start(endpointConfiguration).Result;
        });
    }

    public static void UseNServiceBusInstance(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<IMessageSession>();
    }
}