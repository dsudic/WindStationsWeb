using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class EnvironmentService(WindStationDbContext dbContext) : IEnvironmentService
{
    public List<(DateTime Timestamp, float Value)> GetTemperatureData()
    {
        return dbContext.Environment
            .OrderByDescending(e => e.TimeStamp)
            .Select(env => new { env.TimeStamp, env.Temperature })
            .Take(15)
            .AsEnumerable()
            .Select(env => (env.TimeStamp, env.Temperature))
            .ToList();
    }

    public async Task<float> GetLatestTemperatureAsync()
    {
        return await dbContext.Environment.OrderByDescending(env => env.TimeStamp).Select(env => env.Temperature).FirstAsync();
    }

    public List<(DateTime Timestamp, float Value)> GetHumidityData()
    {
        return dbContext.Environment
            .OrderByDescending(e => e.TimeStamp)
            .Select(env => new { env.TimeStamp, env.Humidity })
            .Take(15)
            .AsEnumerable()
            .Select(env => (env.TimeStamp, env.Humidity))
            .ToList();
    }

    public async Task<float> GetLatestHumidityAsync()
    {
        return await dbContext.Environment.OrderByDescending(env => env.TimeStamp).Select(env => env.Humidity).FirstAsync();
    }

    public List<(DateTime Timestamp, float Value)> GetPressureData()
    {
        return dbContext.Environment
            .OrderByDescending(e => e.TimeStamp)
            .Select(env => new { env.TimeStamp, env.Pressure })
            .Take(15)
            .AsEnumerable()
            .Select(env => (env.TimeStamp, env.Pressure))
            .ToList();
    }

    public async Task<float> GetLatestPressureAsync()
    {
        return await dbContext.Environment.OrderByDescending(env => env.TimeStamp).Select(env => env.Pressure).FirstAsync();
    }
}
