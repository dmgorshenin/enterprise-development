using DispatchService.Model;

namespace DispatchService.Api.DTO;

/// <summary>
/// DTO маршрута
/// </summary>
public class RouteDTO
{
    /// <summary>
    /// Идентификатор маршрута
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Назначенный транспорт
    /// </summary>
    public required Transport AssignedTransport { get; set; }

    /// <summary>
    /// Назначенный водитель
    /// </summary>
    public required Driver AssignedDriver { get; set; }

    /// <summary>
    /// Время начала маршрута 
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Время окончания маршрута
    /// </summary>
    public required DateTime EndTime { get; set; }
}
