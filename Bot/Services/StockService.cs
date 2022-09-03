using Bot.Clients;
using Bot.Models;

namespace Bot.Services;

public class StockService : IStockService
{
    private readonly IStockRestClient _stockRestClient;

    public StockService(IStockRestClient stockRestClient)
    {
        _stockRestClient = stockRestClient;
    }

    public Task<List<Stock>> GetStockByCodeAsync(string stockCode)
    {
       return _stockRestClient.GetStocksAsync(stockCode);
    }
}