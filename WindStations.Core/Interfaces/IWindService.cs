using WindStations.Core.DTOs;

namespace WindStations.Core.Interfaces;
public interface IWindService
{
    public List<WindDTO> GetWindData();
    public Task<float> GetLatestDirectionAsync();
    public Task<float> GetLatestAvgSpeedAsync();
}
