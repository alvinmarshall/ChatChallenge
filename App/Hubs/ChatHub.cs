using App.DTO;
using App.Services;
using Microsoft.AspNetCore.SignalR;

namespace App.Hubs;

public class ChatHub : Hub
{
    public const string RoomMessageMethod = "ReceiveMessage";
    public const string SendMessageMethod = "SendMessage";
    public const string JoinGroupMethod = "JoinGroup";
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(Guid roomId, Guid userId, string message)
    {
        var messageDto = new ChatRoomMessageDto
        {
            Message = message,
            RoomId = roomId,
            UserId = userId,
        };
        var hubDto = await _chatService.ParseMessage(messageDto);
        await Clients.Group(roomId.ToString()).SendAsync(RoomMessageMethod, hubDto);
    }

    public async Task JoinGroup(Guid roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
    }

    public async Task SendMessageToGroup(Guid roomId, Guid userId, string message)
    {
        var hubDto = new ChatRoomHubDto()
        {
            Message = message,
            ChatRoomId = roomId,
            SenderId = userId,
            Sender = "Bot",
            CreatedAt = DateTime.UtcNow
        };
        await Clients.Group(roomId.ToString()).SendAsync(RoomMessageMethod, hubDto);
    }
}