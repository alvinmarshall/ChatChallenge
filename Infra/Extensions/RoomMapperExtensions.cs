using Domain.Model;
using Infra.Entities;

namespace Infra.Extensions;

public static class RoomMapperExtensions
{
    public static ChatRoom ToChatRoom(this RoomEntity input)
    {
        return new ChatRoom
        {
            Id = input.Id,
            Name = input.Name,
            Secret = input.Secret,
            Users = input.Users.ToChatUsers().ToList()
        };
    }

    public static RoomEntity FromChatRoom(this ChatRoom input)
    {
        return new RoomEntity
        {
            Id = input.Id,
            Name = input.Name,
            Secret = input.Secret,
            Users = input.Users.FromChatUsers().ToList()
        };
    }
}