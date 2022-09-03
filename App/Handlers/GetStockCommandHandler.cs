using App.DTO;
using BotCommands.Commands;
using NServiceBus;

namespace App.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    public async Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        var hubDto = new ChatRoomHubDto()
        {
            Message = message.Message,
            Sender = message.Sender,
            ChatRoomId = message.ChatRoomId,
            CreatedAt = message.CreatedAt
        };
        //notify message via signalR-hub
    }
}