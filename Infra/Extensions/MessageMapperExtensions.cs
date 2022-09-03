using Domain.Model;
using Infra.Entities;

namespace Infra.Extensions;

public static class MessageMapperExtensions
{
    public static ChatMessage ToChatMessage(this MessageEntity input)
    {
        return new ChatMessage
        {
            message = input.message,
            User = input.UserEntity.ToChatUser()
        };
    }

    public static MessageEntity FromChatMessage(this ChatMessage input)
    {
        return new MessageEntity
        {
            message = input.message,
            UserEntity = input.User.FromChatUser()
        };
    }
}