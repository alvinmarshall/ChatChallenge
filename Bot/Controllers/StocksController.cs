using Bot.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bot.Controllers;

[Route("[controller]")]
[ApiController]
public class StocksController : Controller
{
    private readonly IStockRestClient _restClient;
    // GET
    public StocksController(IStockRestClient restClient)
    {
        _restClient = restClient;
    }

    [HttpGet]
    public  async Task<ActionResult> Index()
    {
        var stocks = await _restClient.GetStocksAsync("aapl.us");
        return Ok(stocks);
    }
}