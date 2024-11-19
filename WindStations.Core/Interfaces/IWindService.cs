using WindStations.Core.DTOs;

namespace WindStations.Core.Interfaces;
public interface IWindService
{
    public Task<List<WindDTO>> GetWindDataAsync();
    public Task<float> GetLatestDirectionAsync();
    public Task<float?> GetLatestAvgSpeedAsync();
}
