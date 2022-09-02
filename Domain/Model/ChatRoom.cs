namespace Domain.Model;

public record ChatRoom
{
    public string Name { get; set; }
    public string Secret { get; set; }
    public ICollection<ChatUser> Users { get; set; }
}