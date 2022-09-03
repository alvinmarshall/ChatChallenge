using App.Exceptions;
using Bot.Clients;
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
builder.Services.AddScoped<IStockRestClient, StockRestClient>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddSingleton<IStockRestClient, StockRestClient>();

var app = builder.Build();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();