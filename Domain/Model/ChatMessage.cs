namespace Domain.Model;

public record ChatMessage
{
    public Guid Id { get; set; }
    public string message { get; set; }
    public ChatUser User { get; set; }
    public DateTime CreatedAt { get; set; }
    public ChatRoom Room { get; set; }
}