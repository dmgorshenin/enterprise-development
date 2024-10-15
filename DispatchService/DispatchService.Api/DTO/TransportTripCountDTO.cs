using DispatchService.Model;

namespace DispatchService.Api.DTO;

/// <summary>
/// DTO для хранения информации о транспортном средстве и количестве его поездок.
/// </summary>
public class TransportTripCountDTO
{
    /// <summary>
    /// Получает или устанавливает транспортное средство.
    /// </summary>
    public Transport? Transport { get; set; }

    /// <summary>
    /// Получает или устанавливает количество поездок для данного транспортного средства.
    /// </summary>
    public int TripCount { get; set; }
}
