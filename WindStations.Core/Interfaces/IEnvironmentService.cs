using WindStations.Core.DTOs;

namespace WindStations.Core.Interfaces;
public interface IEnvironmentService
{
    public Task<List<RecordDTO>> GetTemperatureDataAsync();
    public Task<float?> GetLatestTemperatureAsync();
    public Task<List<RecordDTO>> GetHumidityDataAsync();
    public Task<float?> GetLatestHumidityAsync();
    public Task<List<RecordDTO>> GetPressureDataAsync();
    public Task<float?> GetLatestPressureAsync();
}
