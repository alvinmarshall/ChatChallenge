using Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class ChatAppContext : DbContext, IChatAppContext
{
    public ChatAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
}