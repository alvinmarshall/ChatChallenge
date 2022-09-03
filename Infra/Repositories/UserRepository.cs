using Domain.Model;
using Domain.Repository;
using Infra.Context;
using Infra.Entities;
using Infra.Extensions;

namespace Infra.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(ChatAppContext context) : base(context)
    {
    }

    public async Task<ChatUser?> GetUser(Guid id)
    {
        var entity = await GetByIdAsync(id);
        return entity?.ToChatUser();
    }

    public async Task<ChatUser?> GetByName(string name)
    {
        var entity = await Task.FromResult(Find(entity => entity.Name == name).FirstOrDefault());
        return entity?.ToChatUser();
    }

    public async Task<ChatUser> CreateUser(ChatUser user)
    {
        var entity = await SaveAsync(user.FromChatUser());
        return entity.ToChatUser();
    }
}