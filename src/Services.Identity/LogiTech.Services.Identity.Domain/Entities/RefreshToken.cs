using LogiTech.BuildingBlocks.Domain;

namespace LogiTech.Services.Identity.Domain.Entities;

public class RefreshToken : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = default!;
    public DateTime ExpiresAtUtc { get; private set; }
    public bool IsRevoked { get; private set; }

    public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAtUtc;

    private RefreshToken() { } // For EF Core

    public RefreshToken(Guid id, Guid userId, string token, DateTime expiresAtUtc)
        : base(id)
    {
        UserId = userId;
        Token = token;
        ExpiresAtUtc = expiresAtUtc;
        IsRevoked = false;
    }

    public void Revoke()
    {
        IsRevoked = true;
        UpdateModifiedDate();
    }
}