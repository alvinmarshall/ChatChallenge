using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatRoomService
{
    Task<ChatMessage> SaveMessage(ChatRoomMessageDto input);
    Task<ChatRoom> AddRoom(CreateRoomDto input);
    Task<ChatRoom> GetRoomById(Guid Id);
    Task<List<ChatRoom>> Rooms();
    Task<ChatRoom> JoinRoom(ChatRoom input);
}