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

    public static List<RoomEntity> FromChatRooms(this IEnumerable<ChatRoom> input)
    {
        return input.Select(room => room.FromChatRoom()).ToList();
    }

    public static List<ChatRoom> ToChatRooms(this IEnumerable<RoomEntity> input)
    {
        return input.Select(room => room.ToChatRoom()).ToList();
    }
}