using Domain.Model;
using Infra.Entities;

namespace Infra.Extensions;

public static class UserMapperExtensions
{
    public static ChatUser ToChatUser(this UserEntity input)
    {
        return new ChatUser
        {
            Id = input.Id,
            Name = input.Name
        };
    }

    public static UserEntity FromChatUser(this ChatUser input)
    {
        return new UserEntity
        {
            Id = input.Id,
            Name = input.Name
        };
    }

    public static IEnumerable<ChatUser> ToChatUsers(this IEnumerable<UserEntity> input)
    {
        return input.Select(entity => entity.ToChatUser());
    }

    public static IEnumerable<UserEntity> FromChatUsers(this IEnumerable<ChatUser> input)
    {
        return input.Select(user => user.FromChatUser());
    }
}