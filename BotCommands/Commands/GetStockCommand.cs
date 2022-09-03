using NServiceBus;

namespace BotCommands.Commands;

public class GetStockCommand : ICommand
{
    public string Id { get; set; }
    public string Stock { get; set; }
    public Guid ChatRoomId { get; set; }
    
}