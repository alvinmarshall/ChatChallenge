using App.DTO;
using App.Exceptions;
using Domain.Model;
using Domain.Repository;

namespace App.Services;

public class ChatRoomService : IChatRoomService
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatRoomService(IChatRoomRepository chatRoomRepository, IChatMessageRepository chatMessageRepository)
    {
        _chatRoomRepository = chatRoomRepository;
        _chatMessageRepository = chatMessageRepository;
    }


    public async Task<ChatMessage> SaveMessage(ChatRoomMessageDto input)
    {
        var room = await _chatRoomRepository.GetBySecreteAsync(input.Secret);
        if (room is null) throw new RecordNotFoundException("Chat Secret Not Found");
        var chatMessage = new ChatMessage
        {
            message = input.Message,
            CreatedAt = DateTime.UtcNow,
            Room = room
        };
        return await _chatMessageRepository.SaveMessageAsync(chatMessage);
    }
}