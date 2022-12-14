using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

[Table("users")]
public class UserEntity : BaseEntity
{
    [Column(name: "name")] public string Name { get; set; }
    public virtual ICollection<RoomEntity> Rooms { get; set; } = Enumerable.Empty<RoomEntity>().ToList();
}