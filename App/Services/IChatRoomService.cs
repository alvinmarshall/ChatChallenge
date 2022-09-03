using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatRoomService
{
    Task<ChatMessage> SaveMessage(ChatRoomMessageDto input);
}