using App.DTO;
using Domain.Model;

namespace App.Services;

public interface IChatService
{
    Task<ChatRoomHubDto> ParseMessage(ChatRoomMessageDto input);
    Task<ChatRoom> AddRoom(CreateRoomDto input);
    Task<List<ChatRoom>> GetRooms();
    Task<List<ChatMessage>> GetRoomMessages(Guid id);
}