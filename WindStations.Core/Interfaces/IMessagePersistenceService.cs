using WindStations.Core.Models;

namespace WindStations.Core.Interfaces;
public interface IMessagePersistenceService
{
    public Task SaveAsync(byte[] payload);
    public Task<DateTime> GetLatestTimestampAsync();
}
