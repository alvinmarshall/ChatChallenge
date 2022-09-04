using Bot.Clients;
using BotCommands.Commands;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Bot.Controllers;

[Route("[controller]")]
[ApiController]
public class StocksController : Controller
{
    private readonly IMessageSession _messageSession;

    private readonly IStockRestClient _restClient;
    // GET
    public StocksController(IStockRestClient restClient, IMessageSession messageSession)
    {
        _restClient = restClient;
        _messageSession = messageSession;
    }

    [HttpGet]
    public  async Task<ActionResult> Index()
    {
        var stocks = await _restClient.GetStocksAsync("aapl.us");
        var stock = stocks.First();
        var command = new GetStockCommand
        {
            Sender = "bot",
            Message = $"stock is {stock.Open} per share",
            ChatRoomId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
        await _messageSession.Send(command);
        return Ok(stocks);
    }
}