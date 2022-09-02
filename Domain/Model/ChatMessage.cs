namespace Domain.Model;

public record ChatMessage
{
    public string message { get; set; }
    public ChatUser User { get; set; }
}