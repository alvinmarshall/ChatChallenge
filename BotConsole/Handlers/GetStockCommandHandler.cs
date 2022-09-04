using BotCommands.Commands;
using NServiceBus;

namespace BotConsole.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    public async Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        Console.WriteLine("message: {0}", message);
    }
}