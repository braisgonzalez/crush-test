namespace Users.Domain;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() { }

    private User(Guid id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public static User Create(string email, string name)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        return new User(Guid.NewGuid(), email.Trim().ToLowerInvariant(), name.Trim());
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        Name = name.Trim();
    }
}