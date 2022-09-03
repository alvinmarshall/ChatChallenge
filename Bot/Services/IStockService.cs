using Bot.Models;

namespace Bot.Services;

public interface IStockService
{
    Task<List<Stock>> GetStockByCodeAsync(string stockCode);
}