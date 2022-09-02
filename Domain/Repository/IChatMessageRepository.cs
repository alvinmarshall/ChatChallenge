using Domain.Model;

namespace Domain.Repository;

public interface IChatMessageRepository
{
    Task<ChatMessage> SaveMessageAsync(ChatMessage chatMessage);
    
}