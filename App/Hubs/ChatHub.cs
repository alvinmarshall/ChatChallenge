using App.DTO;
using App.Services;
using Microsoft.AspNetCore.SignalR;

namespace App.Hubs;

public class ChatHub : Hub
{
    public const string ON_MESSAGE_RECEIVED = "RECIEVED_FROM_CHAT_ROOM";
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string chatRoomSecret, Guid userId, string message)
    {
        var messageDto = new ChatRoomMessageDto
        {
            Message = message,
            Secret = chatRoomSecret,
            UserId = userId
        };
        var hubDto = await _chatService.ParseMessage(messageDto);
        await Clients.Group(chatRoomSecret).SendAsync(ON_MESSAGE_RECEIVED, hubDto);
    }
}