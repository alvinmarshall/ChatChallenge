using Domain.Model;
using Domain.Repository;
using Infra.Context;
using Infra.Entities;
using Infra.Extensions;
using Infra.Specifications;

namespace Infra.Repositories;

public class ChatRoomRepository : BaseRepository<RoomEntity>, IChatRoomRepository
{
    public ChatRoomRepository(ChatAppContext context) : base(context)
    {
    }

    public async Task<ChatRoom?> GetRoom(Guid id)
    {
        var roomEntity = await GetByIdAsync(id, new RoomSpecification());
        return roomEntity?.ToChatRoom();
    }

    public async Task<ChatRoom> Add(ChatRoom input)
    {
        var entity = await AddAsync(input.FromChatRoom());
        return entity.ToChatRoom();
    }

    public async Task<ChatRoom> Update(ChatRoom input)
    {
        var entity = await UpdateAsync(input.FromChatRoom());
        return entity.ToChatRoom();
    }

    public async Task<List<ChatRoom>> GetRooms()
    {
        var entities = await Task.FromResult(GetAll(new RoomSpecification()));
        return entities.ToChatRooms();
    }
}