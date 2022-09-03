using Domain.Model;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<ChatUser?> GetUser(Guid id);
    Task<ChatUser?> GetByName(string name);
    Task<ChatUser> CreateUser(ChatUser user);
}