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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<RoomEntity>()
            .HasMany(r => r.Users)
            .WithMany(u => u.Rooms)
            .UsingEntity<Dictionary<string, object>>(
                "user_rooms",
                ur => ur.HasOne<UserEntity>()
                    .WithMany(),
                    // .OnDelete(DeleteBehavior.Cascade),
                ur => ur.HasOne<RoomEntity>()
                    .WithMany()
                    // .OnDelete(DeleteBehavior.Cascade)
            );
    }
}