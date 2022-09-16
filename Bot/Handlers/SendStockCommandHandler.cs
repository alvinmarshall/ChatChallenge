using Bot.Services;
using BotCommands;
using BotCommands.Commands;
using NServiceBus;

namespace Bot.Handlers;

public class SendStockCommandHandler : IHandleMessages<SendStockCommand>
{
    private readonly IStockService _stockService;

    public SendStockCommandHandler(IStockService stockService)
    {
        _stockService = stockService;
    }

    public async Task Handle(SendStockCommand message, IMessageHandlerContext context)
    {
        var stocks = await _stockService.GetStockByCodeAsync(message.Stock);
        var stock = stocks.FirstOrDefault();
        var botMessage = stock is null
            ? $"{message.Stock} quote not found"
            : $"{message.Stock} quote is {stock.Open} per share";

        var command = new GetStockCommand
        {
            Sender = "bot",
            SenderId = BotInfo.BotId,
            Message = botMessage,
            ChatRoomId = message.ChatRoomId,
            CreatedAt = DateTime.UtcNow
        };
        await context.Send(command);
    }
}