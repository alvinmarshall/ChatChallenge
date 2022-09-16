using NServiceBus;

namespace BotCommands.Commands;

public class GetStockCommand : ICommand
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string Sender { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatRoomId { get; set; }
    public DateTime CreatedAt { get; set; }
}