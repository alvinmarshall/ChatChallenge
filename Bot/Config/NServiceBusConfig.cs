using Bot.Options;
using BotCommands.Commands;
using NServiceBus;
using NServiceBus.ObjectBuilder.MSDependencyInjection;

namespace Bot.Config;

public static class NServiceBusConfig
{
    public static IServiceProvider AddNServiceBus(this IServiceCollection services)
    {
        UpdateableServiceProvider container = null;

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
            endpointConfiguration.UseContainer<ServicesBuilder>(customizations =>
            {
                customizations.ExistingServices(services);
                customizations.ServiceProviderFactory(sc => 
                {
                    container = new UpdateableServiceProvider(sc);
                    return container;
                });
            });
            transport.UseDirectRoutingTopology(QueueType.Quorum);

            transport
                .Routing()
                .RouteToEndpoint(typeof(GetStockCommand), configOptions.GetStockCommandDestination);
            return NServiceBus.Endpoint.Start(endpointConfiguration).Result;
        });
        return container;
    }

    public static void UseNServiceBusInstance(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<IMessageSession>();
    }
}