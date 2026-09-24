using LogiTech.BuildingBlocks.Domain;

namespace LogiTech.Services.Identity.Domain.Entities;

public class User : Entity<Guid>
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; } = true;
    
    // قائمة الـ Refresh Tokens للأمان المتعدد
    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private User() { } // For EF Core

    public User(Guid id, string firstName, string lastName, string email, string passwordHash, string phoneNumber, UserRole role)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public void AddRefreshToken(string token, DateTime expiresAtUtc)
    {
        _refreshTokens.Add(new RefreshToken(Guid.NewGuid(), Id, token, expiresAtUtc));
    }

    public void RevokeRefreshToken(string token)
    {
        var existingToken = _refreshTokens.FirstOrDefault(t => t.Token == token);
        if (existingToken != null)
        {
            existingToken.Revoke();
        }
    }
}

public enum UserRole
{
    Customer = 1,
    Driver = 2,
    FleetManager = 3,
    Admin = 4
}