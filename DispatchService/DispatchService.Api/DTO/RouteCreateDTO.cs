namespace DispatchService.Api.DTO;

/// <summary>
/// 
/// </summary>
public class RouteCreateDTO
{
    /// <summary>
    /// Номер маршрута
    /// </summary>
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Назначенный транспорт
    /// </summary>
    public required int AssignedTransportId { get; set; }

    /// <summary>
    /// Назначенный водитель
    /// </summary>
    public required int AssignedDriverId { get; set; }

    /// <summary>
    /// Время начала маршрута 
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Время окончания маршрута
    /// </summary>
    public DateTime EndTime { get; set; }
}
