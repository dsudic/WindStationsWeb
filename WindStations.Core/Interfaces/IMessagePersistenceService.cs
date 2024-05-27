using WindStations.Core.Models;

namespace WindStations.Core.Interfaces;
public interface IMessagePersistenceService
{
    public Task SaveAsync(byte[] payload);
    public Task<List<Anemometer>> GetAnemometerAsync();
    public Task<List<Vane>> GetVaneAsync();
    public Task<List<Models.Environment>> GetEnvironmentAsync();
    public Task<float> GetBatteryStatusAsync();
}
