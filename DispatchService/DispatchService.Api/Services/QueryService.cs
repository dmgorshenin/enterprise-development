using DispatchService.Api.DTO;
using DispatchService.Model.Entities;
using System.Data;
using Route = DispatchService.Model.Entities.Route;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис запросов
/// </summary>
public class QueryService
{
    private readonly IEntityService<Route, RouteCreateDto, RouteUpdateDto> _routesService;

    public QueryService(IEntityService<Route, RouteCreateDto, RouteUpdateDto> routesService) => _routesService = routesService;

    /// <summary>
    /// Получить список водителей, которые совершили поездки в указанный период.
    /// </summary>
    public async Task<List<Driver>> GetDriversInPeriodAsync(DateTime startDate, DateTime endDate)
    {
        var routes = await _routesService.GetAllAsync();
        return await Task.Run(() =>
            routes.Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
                  .Select(r => r.AssignedDriver)
                  .Distinct()
                  .OrderBy(d => d.FullName)
                  .ToList()
        );
    }

    /// <summary>
    /// Получить суммарное время поездок для каждой модели транспортных средств.
    /// </summary>
    public async Task<List<TotalTripTimesDto>> GetTotalTripTimesByTransportAsync()
    {
        var routes = await _routesService.GetAllAsync();
        return await Task.Run(() =>
            routes.GroupBy(r => r.AssignedTransport.ModelName)
                  .Select(g => new TotalTripTimesDto
                  {
                      ModelName = g.Key,
                      TotalTripTime = TimeSpan.FromHours(g.Sum(r => (r.EndTime - r.StartTime).TotalHours))
                  })
                  .ToList()
        );
    }

    /// <summary>
    /// Получить топ 5 водителей по количеству поездок.
    /// </summary>
    public async Task<List<DriverTripCountDto>> GetTopDriversByTripCountAsync()
    {
        var routes = await _routesService.GetAllAsync();
        return await Task.Run(() =>
            routes.GroupBy(r => r.AssignedDriver.Id)
                  .Select(g => new DriverTripCountDto
                  {
                      Driver = g.First().AssignedDriver,
                      TripCount = g.Count()
                  })
                  .OrderByDescending(g => g.TripCount)
                  .Take(5)
                  .ToList()
        );
    }

    /// <summary>
    /// Получить статистику поездок для каждого водителя.
    /// </summary>
    public async Task<List<DriverTripStatsDto>> GetDriverTripStatsAsync()
    {
        var routes = await _routesService.GetAllAsync();
        return await Task.Run(() =>
            routes.GroupBy(r => r.AssignedDriver)
                  .Select(g => new DriverTripStatsDto
                  {
                      Driver = g.Key,
                      TripCount = g.Count(),
                      AvgTripTime = g.Average(r => (r.EndTime - r.StartTime).TotalHours),
                      MaxTripTime = g.Max(r => (r.EndTime - r.StartTime).TotalHours)
                  })
                  .ToList()
        );
    }

    /// <summary>
    /// Получить топ 5 транспортных средств по количеству поездок в указанный период.
    /// </summary>
    public async Task<List<TransportTripCountDto>> GetTopTransportsByTripCountAsync(DateTime startDate, DateTime endDate)
    {
        var routes = await _routesService.GetAllAsync();
        return await Task.Run(() =>
            routes.Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
                  .GroupBy(r => r.AssignedTransport)
                  .Select(g => new TransportTripCountDto
                  {
                      Transport = g.Key,
                      TripCount = g.Count()
                  })
                  .OrderByDescending(g => g.TripCount)
                  .Take(5)
                  .ToList()
        );
    }
}
