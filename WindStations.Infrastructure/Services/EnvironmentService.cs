using Microsoft.EntityFrameworkCore;
using WindStations.Core.DTOs;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class EnvironmentService(WindStationDbContext dbContext) : IEnvironmentService
{
    public async Task<List<RecordDTO>> GetTemperatureDataAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(e => e.Timestamp)
            .Select(env => new RecordDTO(env.Timestamp, env.Temperature))
            .Take(15)
            .ToListAsync();
    }

    public async Task<float?> GetLatestTemperatureAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(env => env.Timestamp)
            .Select(env => (float?)env.Temperature)
            .FirstOrDefaultAsync();
    }

    public async Task<List<RecordDTO>> GetHumidityDataAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(e => e.Timestamp)
            .Select(env => new RecordDTO(env.Timestamp, env.Humidity))
            .Take(15)
            .ToListAsync();
    }

    public async Task<float?> GetLatestHumidityAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(env => env.Timestamp)
            .Select(env => (float?)env.Humidity)
            .FirstOrDefaultAsync();
    }

    public async Task<List<RecordDTO>> GetPressureDataAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(e => e.Timestamp)
            .Select(env => new RecordDTO(env.Timestamp, env.Pressure))
            .Take(15)
            .ToListAsync();
    }

    public async Task<float?> GetLatestPressureAsync()
    {
        return await dbContext.Environment
            .OrderByDescending(env => env.Timestamp)
            .Select(env => (float?)env.Pressure)
            .FirstOrDefaultAsync();
    }
}
