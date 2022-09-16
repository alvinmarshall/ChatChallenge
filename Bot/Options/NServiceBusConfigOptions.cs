namespace Bot.Options;

public class NServiceBusConfigOptions
{
    public const string SectionName = "NServiceBus";
    public string? ChatBotEndpoint { get; set; }
    public string? GetStockCommandDestination { get; set; }
    public string? TransportConnection { get; set; }
    
}
