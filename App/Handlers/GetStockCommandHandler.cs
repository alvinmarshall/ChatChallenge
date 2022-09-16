using App.DTO;
using App.Hubs;
using BotCommands.Commands;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;

namespace App.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    private readonly IHubContext<ChatHub> _hubContext;


    public GetStockCommandHandler(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        var hubDto = new ChatRoomMessageDto
        {
            Message = message.Message,
            RoomId = message.ChatRoomId,
            UserId = message.SenderId,
            IsBot = true
        };
        await _hubContext.Clients.Group(hubDto.RoomId.ToString()).SendAsync(ChatHub.RoomMessageMethod, hubDto);
        await _hubContext.Clients.All.SendAsync(ChatHub.RoomMessageMethod, hubDto);
        Console.WriteLine($"sent-Message: {hubDto.Message}");
    }
}