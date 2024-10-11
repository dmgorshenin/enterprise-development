using DispatchService.Api.DTO;
using DispatchService.Model;
using System.Data;

namespace DispatchService.Api.Services
{
    public class QueryService(RoutesService routesService)
    {
        public List<Driver> GetDriversInPeriod(DateTime startDate, DateTime endDate)
        {
            return [.. routesService.GetAll()
                .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
                .Select(r => r.AssignedDriver)
                .Distinct()
                .OrderBy(d => d.FullName)];
        }

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
                .Take(5)
                .ToList()];
        }

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
}
