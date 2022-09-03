using App.Exceptions;
using App.Services;
using Infra.Config;
using Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Exception handling
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
        new BadRequestObjectResult(ExceptionConfiguration.HandleValidationExceptionResponse(context));
});

builder.Services.AddDbContext<ChatAppContext>(options =>
{
    var connection = new SqliteConnection("Filename=:memory:");
    connection.Open();
    options.UseSqlite(connection);
});
builder.Services.AddInfrastructureServices();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessagingService, MessagingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();