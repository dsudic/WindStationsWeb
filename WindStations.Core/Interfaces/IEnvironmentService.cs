namespace WindStations.Core.Interfaces;
public interface IEnvironmentService
{
    public List<(DateTime Timestamp, float Value)> GetTemperatureData();
    public Task<float> GetLatestTemperatureAsync();
    public List<(DateTime Timestamp, float Value)> GetHumidityData();
    public Task<float> GetLatestHumidityAsync();
    public List<(DateTime Timestamp, float Value)> GetPressureData();
    public Task<float> GetLatestPressureAsync();
}
