using Domain.Model;

namespace Domain.Repository;

public interface IChatMessageRepository
{
    Task<ChatMessage> SaveMessageAsync(ChatMessage chatMessage);
    Task<List<ChatMessage>> GetByRoomIdAsync(Guid roomId);
}