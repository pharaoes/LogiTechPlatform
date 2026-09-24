namespace LogiTech.BuildingBlocks.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? LastModifiedAtUtc { get; private set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity() { }

    public void UpdateModifiedDate()
    {
        LastModifiedAtUtc = DateTime.UtcNow;
    }
}