namespace App.DTO;

public record ChatRoomMessageDto
{
    public string Message { get; set; }
    public string Secret { get; set; }
    public Guid UserId { get; set; }
}