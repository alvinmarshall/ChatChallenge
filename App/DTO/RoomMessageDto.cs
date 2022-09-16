using Domain.Model;

namespace App.DTO;

public record RoomMessageDto
{
    public string Message { get; set; }
    public ChatUser User { get; set; }
    public Guid RoomId { get; set; }
    public bool CurrentUser { get; set; }
    public DateTime CreatedAt { get; set; }
}