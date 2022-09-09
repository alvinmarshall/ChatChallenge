using App.DTO;
using App.Exceptions;
using BotCommands;
using BotCommands.Commands;
using Domain.Model;
using NServiceBus;

namespace App.Services;

public class ChatService : IChatService
{
    private readonly IChatRoomService _chatRoomService;

    private readonly IMessageService _messageService;
    private readonly IMessageSession _messageSession;


    public ChatService(
        IChatRoomService chatRoomService,
        IMessageService messageService,
        IMessageSession messageSession
        )
    {
        _chatRoomService = chatRoomService;
        _messageService = messageService;
        _messageSession = messageSession;
    }

    public async Task<ChatRoomHubDto> ParseMessage(ChatRoomMessageDto input)
    {
        var chatRoom = await _chatRoomService.GetRoomBySecret(input.Secret);
        if (input.Message.Contains("/stock")) return await PerformBotAction(input.Message, chatRoom.Id);
        var chatMessage = await _chatRoomService.SaveMessage(input);
        return new ChatRoomHubDto()
        {
            Message = chatMessage.message,
            Sender = chatMessage.User.Name,
            SenderId = chatMessage.Id,
            CreatedAt = chatMessage.CreatedAt,
            ChatRoomId = chatMessage.Room.Id
        };
    }

    public Task<ChatRoom> AddRoom(CreateRoomDto input)
    {
        return _chatRoomService.AddRoom(input);
    }

    public Task<List<ChatRoom>> GetRooms()
    {
        return _chatRoomService.Rooms();
    }

    public Task<List<ChatMessage>> GetRoomMessages(Guid id)
    {
        return _messageService.GetRoomMessages(id);
    }

    private async Task<ChatRoomHubDto> PerformBotAction(string message, Guid chatRoomId)
    {
        var hubDto = new ChatRoomHubDto
        {
            Sender = BotInfo.BotName,
            SenderId = BotInfo.BotId,
            CreatedAt = DateTime.UtcNow,
            ChatRoomId = chatRoomId
        };
        try
        {
            var stockCode = GetStockCode(message);
            var command = new SendStockCommand
            {
                Id = Guid.NewGuid().ToString(),
                Stock = stockCode,
                ChatRoomId = chatRoomId
            };
            await _messageSession.Send(command);
            hubDto.Message = "...";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            if (e is not ChatServiceException) throw;
            hubDto.Message = e.Message;
        }

        return hubDto;
    }

    private static string GetStockCode(string message)
    {
        var split = message.Split("=");
        if (split.Length <= 1) throw new ChatServiceException("stock code required!");
        return split[1];
    }
}