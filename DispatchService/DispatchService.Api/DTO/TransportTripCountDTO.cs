using DispatchService.Model;

namespace DispatchService.Api.DTO;

public class TransportTripCountDTO
{
    public Transport? Transport { get; set; }
    public int TripCount { get; set; }
}
