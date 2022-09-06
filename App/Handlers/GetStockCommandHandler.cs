using App.DTO;
using App.Services;
using BotCommands.Commands;
using NServiceBus;

namespace App.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    private readonly IChatRoomService _roomService;

    public GetStockCommandHandler(IChatRoomService roomService)
    {
        _roomService = roomService;
    }

    public Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        Console.WriteLine("Received-Message: {0}", message);
        var hubDto = new ChatRoomHubDto()
        {
            Message = message.Message,
            Sender = message.Sender,
            ChatRoomId = message.ChatRoomId,
            CreatedAt = message.CreatedAt
        };
        Console.WriteLine("message: {0}", hubDto);
        return Task.CompletedTask;
    }
}