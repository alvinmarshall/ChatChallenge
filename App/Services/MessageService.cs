using Domain.Model;
using Domain.Repository;

namespace App.Services;

public class MessageService : IMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public MessageService(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public Task<List<ChatMessage>> GetRoomMessages(Guid roomId)
    {
        return _chatMessageRepository.GetByRoomIdAsync(roomId);
    }
}