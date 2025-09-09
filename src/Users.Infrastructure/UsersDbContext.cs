using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Infrastructure;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var e = modelBuilder.Entity<User>();
        e.HasKey(x => x.Id);
        e.Property(x => x.Email).IsRequired();
        e.Property(x => x.Name).IsRequired();
        e.HasIndex(x => x.Email).IsUnique();
    }
}