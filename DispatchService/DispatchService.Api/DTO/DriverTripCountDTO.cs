using DispatchService.Model;

namespace DispatchService.Api.DTO;

/// <summary>
/// DTO для хранения информации о водителе и количестве его поездок.
/// </summary>
public class DriverTripCountDTO
{
    /// <summary>
    /// Получает или устанавливает водителя.
    /// </summary>
    public Driver? Driver { get; set; }

    /// <summary>
    /// Получает или устанавливает количество поездок для данного водителя.
    /// </summary>
    public int TripCount { get; set; }
}
