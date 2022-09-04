namespace App.DTO;

public record ChatRoomHubDto
{
    public Guid SenderId { get; set; }
    public string Sender { get; set; }
    public string Message { get; set; }
    public Guid ChatRoomId { get; set; }
    public DateTime CreatedAt { get; set; }
}