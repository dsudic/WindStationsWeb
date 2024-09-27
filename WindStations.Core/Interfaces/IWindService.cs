namespace WindStations.Core.Interfaces;
public interface IWindService
{
    public List<(DateTime Timestamp, float MinSpeed, float AvgSpeed, float MaxSpeed, float Direction)> GetWindData();
    public Task<float> GetLatestDirectionAsync();
    public Task<float> GetLatestAvgSpeedAsync();
}
