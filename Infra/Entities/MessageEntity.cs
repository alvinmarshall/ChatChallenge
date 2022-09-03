using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

[Table("messages")]
public class MessageEntity
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    public string message { get; set; }

    [ForeignKey("user_id")] public virtual UserEntity UserEntity { get; set; }

    [ForeignKey(name: "room_id")] public virtual RoomEntity Room { get; set; }

    public DateTime CreatedAt { get; set; }
}