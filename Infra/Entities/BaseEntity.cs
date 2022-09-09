using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities;

public class BaseEntity
{
    [Key, Column(name: "id")] public Guid Id { get; set; }
}