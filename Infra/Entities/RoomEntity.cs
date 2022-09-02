using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

[Table("rooms")]
public class RoomEntity
{
    [Key, Column(name: "id")] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Secret { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}