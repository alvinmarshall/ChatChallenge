using Domain.Model;
using Infra.Entities;

namespace Infra.Extensions;

public static class MessageMapperExtensions
{
    public static ChatMessage ToChatMessage(this MessageEntity input)
    {
        return new ChatMessage
        {
            Id = input.Id,
            message = input.message,
            User = input.UserEntity.ToChatUser(),
            CreatedAt = input.CreatedAt,
            Room = input.Room.ToChatRoom()
        };
    }

    public static MessageEntity FromChatMessage(this ChatMessage input)
    {
        return new MessageEntity
        {
            Id = input.Id,
            message = input.message,
            UserEntity = input.User.FromChatUser(),
            CreatedAt = input.CreatedAt,
            Room = input.Room.FromChatRoom()
        };
    }
}