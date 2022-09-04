using NServiceBus;
using Endpoint = NServiceBus.Endpoint;

namespace App.Config;

public static class NServiceBusConfig
{
    public static void AddNServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSession>(provider =>
        {
            var endpointConfiguration = new EndpointConfiguration("Chat");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            var configuration = provider.GetRequiredService<IConfiguration>();
            transport.ConnectionString(configuration["RabbitMq:Connection"]);
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            return Endpoint.Start(endpointConfiguration)
                .GetAwaiter()
                .GetResult();
        });
    }
}