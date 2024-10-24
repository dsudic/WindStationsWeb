namespace WindStations.Core.Models;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}
