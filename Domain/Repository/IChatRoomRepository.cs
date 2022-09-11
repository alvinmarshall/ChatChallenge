using Domain.Model;

namespace Domain.Repository;

public interface IChatRoomRepository
{
    Task<ChatRoom?> GetRoom(Guid secret);
    Task<ChatRoom> Add(ChatRoom input);
    Task<List<ChatRoom>> GetRooms();
}