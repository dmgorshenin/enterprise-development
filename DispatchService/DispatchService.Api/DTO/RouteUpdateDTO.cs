namespace DispatchService.Api.DTO;

/// <summary>
/// DTO для обновления маршрута
/// </summary>
public class RouteUpdateDTO
{
    /// <summary>
    /// Идентификатор маршрута
    /// </summary>
    public required int RouteId { get; set; }

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
