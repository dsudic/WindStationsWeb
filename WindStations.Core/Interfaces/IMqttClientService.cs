namespace WindStations.Core.Interfaces;
public interface IMqttClientService
{
    public Task ConnectAndSubscribe();
}
