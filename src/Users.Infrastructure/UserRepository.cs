using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Infrastructure;

public sealed class UserRepository(UsersDbContext db) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await db.Users.AddAsync(user, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await db.Users.FindAsync(new object?[] { id }, ct);

    public async Task<IReadOnlyList<User>> ListAsync(CancellationToken ct = default)
        => await db.Users.AsNoTracking().OrderBy(u => u.CreatedAt).ToListAsync(ct);

    public async Task UpdateAsync(User user, CancellationToken ct = default)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync(ct);
    }
}