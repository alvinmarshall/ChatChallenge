using Bot.Models;

namespace Bot.Clients;

public interface IStockRestClient
{
    Task<List<Stock>> GetStocks(string stockName);
}