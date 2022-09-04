using App.DTO;
using App.Hubs;
using BotCommands.Commands;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;

namespace App.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    private readonly IHubContext<ChatHub> _context;

    public GetStockCommandHandler(IHubContext<ChatHub> context)
    {
        _context = context;
    }

    public async Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        var hubDto = new ChatRoomHubDto()
        {
            Message = message.Message,
            Sender = message.Sender,
            ChatRoomId = message.ChatRoomId,
            CreatedAt = message.CreatedAt
        };
        await _context.Clients.Group(message.ChatRoomId.ToString()).SendAsync(ChatHub.ON_MESSAGE_RECEIVED, hubDto);
    }
}