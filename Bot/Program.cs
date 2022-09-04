using Bot.Clients;
using Bot.Config;
using Bot.Exceptions;
using Bot.Options;
using Bot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton(provider =>
{
    var requiredService = provider.GetRequiredService<IConfiguration>();
    var instance = new StockConfigOption();
    requiredService.Bind(StockConfigOption.SectionName, instance);
    return instance;
});
builder.Services.AddHttpClient<StockService>();
builder.Services.AddNServiceBus();
builder.Services.AddScoped<IStockRestClient, StockRestClient>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddSingleton<IStockRestClient, StockRestClient>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseNServiceBusInstance();
app.Run();