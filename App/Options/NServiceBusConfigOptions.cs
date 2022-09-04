namespace App.Options;

public class NServiceBusConfigOptions
{
    public const string SectionName = "NServiceBus";
    public string ChatEndpoint { get; set; }
    public string SendStockCommandDestination { get; set; }
    public string TransportConnection { get; set; }
    
}
