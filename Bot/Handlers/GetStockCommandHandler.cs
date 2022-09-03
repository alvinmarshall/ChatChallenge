using BotCommands.Commands;
using NServiceBus;

namespace Bot.Handlers;

public class GetStockCommandHandler : IHandleMessages<GetStockCommand>
{
    public Task Handle(GetStockCommand message, IMessageHandlerContext context)
    {
        throw new NotImplementedException();
    }
}