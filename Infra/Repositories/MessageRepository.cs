using Domain.Model;
using Domain.Repository;
using Infra.Context;
using Infra.Entities;
using Infra.Extensions;
using Infra.Specifications;

namespace Infra.Repositories;

public class MessageRepository : BaseRepository<MessageEntity>, IChatMessageRepository
{
    public MessageRepository(ChatAppContext context) : base(context)
    {
    }

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage chatMessage)
    {
        var entity = await AddAsync(chatMessage.FromChatMessage());
        return entity.ToChatMessage();
    }

    public async Task<List<ChatMessage>> GetByRoomIdAsync(Guid roomId)
    {
        var entities = await Task.FromResult(
            Find(entity => entity.Room.Id == roomId, new MessageSpecification()).ToList()
        );
        return entities.ToChatMessages();
    }
}