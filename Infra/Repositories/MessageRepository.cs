using Domain.Model;
using Domain.Repository;
using Infra.Context;
using Infra.Entities;
using Infra.Extensions;

namespace Infra.Repositories;

public class MessageRepository : BaseRepository<MessageEntity>, IChatMessageRepository
{
    public MessageRepository(ChatAppContext context) : base(context)
    {
    }

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage chatMessage)
    {
        var entity = await SaveAsync(chatMessage.FromChatMessage());
        return entity.ToChatMessage();
    }
}