namespace App.Auth;

public record UserSession
{
    public Guid Id { get; set; }
    public string Role { get; set; } = "User";
}