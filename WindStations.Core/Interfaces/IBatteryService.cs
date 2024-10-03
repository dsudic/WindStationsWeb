namespace WindStations.Core.Interfaces;
public interface IBatteryService
{
    public Task<float> GetBatteryVoltageAsync();
}
