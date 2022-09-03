using System.Globalization;
using Bot.Models;
using CsvHelper;

namespace Bot.Services;

public static class ParseStockCsv
{
    public static List<Stock> GetStocks(Stream content)
    {
        using var reader = new StreamReader(content);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<Stock>().ToList();
    }
}