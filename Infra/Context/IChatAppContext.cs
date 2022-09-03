using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public interface IChatAppContext
{
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
}