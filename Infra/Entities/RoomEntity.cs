using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

[Table("rooms")]
public class RoomEntity : BaseEntity
{
    public string Name { get; set; }
    public string Secret { get; set; }
    public virtual ICollection<UserEntity> Users { get; set; } = Enumerable.Empty<UserEntity>().ToList();
}