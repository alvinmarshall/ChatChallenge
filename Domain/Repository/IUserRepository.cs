using Domain.Model;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<ChatUser> GetUser(Guid id);
    Task<ChatUser> CreateUser(ChatUser user);
}