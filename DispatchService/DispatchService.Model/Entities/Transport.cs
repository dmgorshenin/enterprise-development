using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchService.Model.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
[Table("transport")]
public class Transport
{
    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Госномер автомобиля
    /// </summary>
    [Column("license_plate")]
    [MaxLength(10)]
    [Required]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Вид транспорта
    /// </summary>
    [Column("vehicle_type")]
    [Required]
    [EnumDataType(typeof(VehicleType))]
    public required VehicleType Type { get; set; }

    /// <summary>
    /// Название модели транспорта
    /// </summary>
    [Column("model_name")]
    [MaxLength(50)]
    [Required]
    public required string ModelName { get; set; }

    /// <summary>
    /// Тип транспорта (низкопольный / высокопольный) 
    /// </summary>
    [Column("is_low_floor")]
    [Required]
    public required bool IsLowFloor { get; set; }

    /// <summary>
    /// Максимальная вместительность
    /// </summary>
    [Column("max_capacity")]
    [Required]
    public required int MaxCapacity { get; set; }

    /// <summary>
    /// Год выпуска транспорта
    /// </summary>
    [Column("year_of_manufacture")]
    [Range(1900, 2100)]
    [Required]
    public required int YearOfManufacture { get; set; }


}

///<summary>
/// Виды транспорта
///</summary>
public enum VehicleType
{
    Bus,
    Tram,
    Trolleybus
}