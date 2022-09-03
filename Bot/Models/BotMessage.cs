namespace Bot.Models;

public record BotMessage
{
    public string Id { get; set; }
    public string Message { get; set; }
    public Guid ChatRoomId { get; set; }
}