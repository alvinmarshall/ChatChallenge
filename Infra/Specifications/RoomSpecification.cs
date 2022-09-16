using Infra.Entities;
using Infra.Specifications.Base;

namespace Infra.Specifications;

public class RoomSpecification : BaseSpecification<RoomEntity>
{
    public RoomSpecification()
    {
        AddInclude(entity => entity.Users);
    }
}