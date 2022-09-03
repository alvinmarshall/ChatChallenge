using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatService
{
    Task ParseMessage(ChatRoomMessageDto input);
    Task<ChatRoom> AddRoom(CreateRoomDto input);
}