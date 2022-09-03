using Domain.Repository;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Config;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IChatMessageRepository, MessageRepository>();
        return services;
    }
}