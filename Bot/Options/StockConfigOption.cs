namespace Bot.Options;

public class StockConfigOption
{
    public const string SectionName = "StockApi";
    public string BaseUrl { get; set; }
    public string Format { get; set; } = "csv";
}