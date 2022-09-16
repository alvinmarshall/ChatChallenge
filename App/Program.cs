using App.Auth;
using App.Config;
using App.Exceptions;
using App.Hubs;
using App.Services;
using Infra.Config;
using Infra.Context;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddScoped<AuthenticationStateProvider, SimpleAuthenticationStateProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Exception handling
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
        new BadRequestObjectResult(ExceptionConfiguration.HandleValidationExceptionResponse(context));
});

builder.Services.AddDbContext<ChatAppContext>(options => { options.UseSqlite("Data source=chat.db"); });

builder.Services.AddInfrastructureServices();
builder.Services.AddNServiceBus();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ChatAppContext>();
    // db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.MapBlazorHub();
// app.UseEndpoints(routes =>
// {
//     routes.MapHub<ChatHub>("/chathub");
// });
app.MapControllers();
app.UseNServiceBusInstance();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");


app.Run();