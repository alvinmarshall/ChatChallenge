using Domain.Model;
using Domain.Repository;
using Infra.Context;
using Infra.Entities;
using Infra.Extensions;

namespace Infra.Repositories;

public class ChatRoomRepository : BaseRepository<RoomEntity>, IChatRoomRepository
{
    public ChatRoomRepository(ChatAppContext context) : base(context)
    {
    }

    public async Task<ChatRoom?> GetBySecreteAsync(string secret)
    {
        var roomEntity = await Task.FromResult(Find(entity => entity.Secret == secret).FirstOrDefault());
        return roomEntity?.ToChatRoom();
    }

    public async Task<ChatRoom> Add(ChatRoom input)
    {
        var entity = await SaveAsync(input.FromChatRoom());
        return entity.ToChatRoom();
    }

    public async Task<List<ChatRoom>> GetRooms()
    {
        var entities = await Task.FromResult(GetAll());
        return entities.ToChatRooms();
    }
}