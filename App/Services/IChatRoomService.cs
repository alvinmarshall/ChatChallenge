using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatRoomService
{
    Task<ChatMessage> SaveMessage(ChatRoomMessageDto input);
    Task<ChatRoom> AddRoom(CreateRoomDto input);
    Task<ChatRoom> GetRoomBySecret(string secret);
    Task<List<ChatRoom>> Rooms();
}