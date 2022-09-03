namespace Domain.Model;

public record ChatRoom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Secret { get; set; }
    public ICollection<ChatUser> Users { get; set; } = Enumerable.Empty<ChatUser>().ToList();
}