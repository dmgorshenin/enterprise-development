namespace DispatchService.Api.DTO;

/// <summary>
/// DTO траспорта
/// </summary>
public class TransportDTO
{
    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Вид транспорта
    /// </summary>
    public required VehicleType Type { get; set; }

    /// <summary>
    /// Название модели транспорта
    /// </summary>
    public required string ModelName { get; set; }

    /// <summary>
    /// Тип транспорта (низкопольный / высокопольный) 
    /// </summary>
    public required bool IsLowFloor { get; set; }

    /// <summary>
    /// Максимальная вместительность
    /// </summary>
    public required int MaxCapacity { get; set; }

    /// <summary>
    /// Год выпуска транспорта
    /// </summary>
    public required int YearOfManufacture { get; set; }
}

/// <summary>
/// Виды транспорта
/// </summary>
public enum VehicleType
{
    Bus,
    Tram,
    Trolleybus
}

