using Microsoft.AspNetCore.SignalR;

namespace WindStations.Infrastructure.Hubs;
public class DataUpdatesHub : Hub<IDataUpdateClient>
{
}
