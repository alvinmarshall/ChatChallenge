using App.DTO;

namespace App.Services;

public interface IChatService
{
    Task ParseMessage(ChatRoomMessageDto input);
}