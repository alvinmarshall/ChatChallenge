using App.DTO;

namespace App.Services;

public class ChatService : IChatService
{
    private readonly IChatRoomService _chatRoomService;

    public ChatService(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }

    public async Task ParseMessage(ChatRoomMessageDto input)
    {
        var message = await _chatRoomService.SaveMessage(input);
        
    }
}