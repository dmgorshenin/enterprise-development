using DispatchService.Model;

namespace DispatchService.Api.DTO
{
    public class DriverTripStatsDTO
    {
        public Driver? Driver { get; set; }
        public int TripCount { get; set; }
        public double AvgTripTime { get; set; }
        public double MaxTripTime { get; set; }
    }
}
