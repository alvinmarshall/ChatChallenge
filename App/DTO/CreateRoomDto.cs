namespace App.DTO;

public record CreateRoomDto
{
    public string Name { get; set; }
    public string Secret { get; set; }
}