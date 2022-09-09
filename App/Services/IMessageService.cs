using Domain.Model;

namespace App.Services;

public interface IMessageService
{
    Task<List<ChatMessage>> GetRoomMessages(Guid roomId);
}