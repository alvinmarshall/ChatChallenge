namespace BotCommands.Commands;

public class SendStockCommand
{
    public string Id { get; set; }
    public string Stock { get; set; }
    public Guid ChatRoomId { get; set; }
}