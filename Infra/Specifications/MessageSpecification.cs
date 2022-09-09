using Infra.Entities;
using Infra.Specifications.Base;

namespace Infra.Specifications;

public class MessageSpecification : BaseSpecification<MessageEntity>
{
    public MessageSpecification()
    {
        AddInclude(entity => entity.UserEntity);
        AddInclude(entity => entity.Room);
    }
}