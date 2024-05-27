namespace WindStations.Core.Models;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public DateTime TimeStamp { get; private set; } = DateTime.Now;
}
