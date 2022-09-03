using Bot.Models;
using Bot.Options;
using Bot.Services;

namespace Bot.Clients;

public class StockRestClient : IStockRestClient
{
    private readonly HttpClient _client = new();
    private readonly StockConfigOption _stockConfigOption;
    private static readonly Dictionary<string, List<Stock>> StockInMemory = new();

    public StockRestClient(StockConfigOption stockConfigOption)
    {
        _stockConfigOption = stockConfigOption;
    }

    public async Task<List<Stock>> GetStocks(string stockName)
    {
        var cacheList = StockInMemory.GetValueOrDefault(stockName);
        if (cacheList is not null) return cacheList;
        var uri = $"{_stockConfigOption.BaseUrl}/q/l/?s={stockName}&f=sd2t2ohlcv&h&e={_stockConfigOption.Format}";
        using var response = await _client.GetAsync(uri);
        await using var stream = await response.Content.ReadAsStreamAsync();
        var stocks = ParseStockCsv.GetStocks(stream);
        StockInMemory.Add(stockName, stocks);
        return stocks;
    }
}