using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

[Table("users")]
public class UserEntity
{
    [Key, Column(name: "id")] public Guid Id { get; set; }
    [Column(name: "name")] public string Name { get; set; }
}