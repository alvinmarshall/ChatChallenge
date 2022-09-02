namespace Domain.Model;

public record ChatUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}