using Domain.Model;

namespace Domain.Repository;

public interface IChatRoomRepository
{
    Task<ChatRoom?> GetBySecreteAsync(string secret);
    Task<ChatRoom> Add(ChatRoom input);
}