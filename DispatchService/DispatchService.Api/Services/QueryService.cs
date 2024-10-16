using DispatchService.Api.DTO;
using DispatchService.Model;
using System.Data;
namespace DispatchService.Api.Services;

/// <summary>
/// Сервис запросов
/// </summary>
/// <param name="routesService">Сервис маршрутов</param>
public class QueryService(RoutesService routesService)
{
    /// <summary>
    /// Получить список водителей, которые совершили поездки в указанный период.
    /// </summary>
    public List<Driver> GetDriversInPeriod(DateTime startDate, DateTime endDate)
    {
        return [.. routesService.GetAll()
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .Select(r => r.AssignedDriver)
            .Distinct()
            .OrderBy(d => d.FullName)];
    }

    /// <summary>
    /// Получить суммарное время поездок для каждой модели транспортных средств.
    /// </summary>
    public List<TotalTripTimesDTO> GetTotalTripTimesByTransport()
    {
        return [.. routesService.GetAll()
            .GroupBy(r => r.AssignedTransport.ModelName)
            .Select(g => new TotalTripTimesDTO
            {
                ModelName = g.Key,
                TotalTripTime = TimeSpan.FromHours(g.Sum(r => (r.EndTime - r.StartTime).TotalHours))
            })];
    }

    /// <summary>
    /// Получить топ 5 водителей по количеству поездок.
    /// </summary>
    public List<DriverTripCountDTO> GetTopDriversByTripCount()
    {
        return [.. routesService.GetAll()
            .GroupBy(r => r.AssignedDriver.Id)
            .Select(g => new DriverTripCountDTO
            {
                Driver = g.First().AssignedDriver,
                TripCount = g.Count()
            })
            .OrderByDescending(g => g.TripCount)
            .Take(5)];
    }

    /// <summary>
    /// Получить статистику поездок для каждого водителя.
    /// </summary>
    public List<DriverTripStatsDTO> GetDriverTripStats()
    {
        return [.. routesService.GetAll()
            .GroupBy(r => r.AssignedDriver)
            .Select(g => new DriverTripStatsDTO
            {
                Driver = g.Key,
                TripCount = g.Count(),
                AvgTripTime = g.Average(r => (r.EndTime - r.StartTime).TotalHours),
                MaxTripTime = g.Max(r => (r.EndTime - r.StartTime).TotalHours)
            })];
    }

    /// <summary>
    /// Получить топ 5 транспортных средств по количеству поездок в указанный период.
    /// </summary>
    public List<TransportTripCountDTO> GetTopTransportsByTripCount(DateTime startDate, DateTime endDate)
    {
        return [.. routesService.GetAll()
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .GroupBy(r => r.AssignedTransport)
            .Select(g => new TransportTripCountDTO
            {
                Transport = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(g => g.TripCount)
            .Take(5)];
    }
}
