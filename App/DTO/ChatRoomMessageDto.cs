namespace App.DTO;

public record ChatRoomMessageDto
{
    public string Message { get; set; }
    public string Secret { get; set; }
    public string UserId { get; set; }
}