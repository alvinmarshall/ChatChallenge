using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatRoomService
{
    Task<ChatMessage> SaveMessage(ChatRoomMessageDto input);
    Task<ChatRoom> AddRoom(CreateRoomDto input);
    Task<List<ChatRoom>> Rooms();
}