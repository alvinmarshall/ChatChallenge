namespace BotConsole.Options;

public class NServiceBusConfigOptions
{
    public const string SectionName = "NServiceBus";
    public string ChatBotEndpoint { get; set; }
    public string ChatBotDestination { get; set; }
    public string TransportConnection { get; set; }
    
}
