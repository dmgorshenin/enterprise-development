using DispatchService.Model.Entities;
namespace DispatchService.Api.DTO;

/// <summary>
/// DTO для хранения суммарного времени поездок транспортного средства.
/// </summary>
public class TotalTripTimesDto
{
    /// <summary>
    /// Получает или устанавливает название модели транспортного средства.
    /// </summary>
    public string? ModelName { get; set; }

    /// <summary>
    /// Получает или устанавливает тип транспортного средства.
    /// </summary>
    public VehicleType? Type { get; set; }

    /// <summary>
    /// Получает или устанавливает общее время поездок для данной модели транспортного средства.
    /// </summary>
    public TimeSpan TotalTripTime { get; set; }
}
