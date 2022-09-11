namespace App.DTO;

public record ChatRoomMessageDto
{
    public string Message { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
}