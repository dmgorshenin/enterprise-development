using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchService.Model.Entities;


/// <summary>
/// Маршрут
/// </summary>
[Table("route")]
public class Route
{
    /// <summary>
    /// Идентификатор маршрута
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    [Column("route_number")]
    [Required]
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Назначенный транспорт
    /// </summary>
    [Column("assigned_transport")]
    [Required]
    public required Transport AssignedTransport { get; set; }

    /// <summary>
    /// Назначенный водитель
    /// </summary>
    [Column("assigned_driver")]
    [Required]
    public required Driver AssignedDriver { get; set; }

    /// <summary>
    /// Время начала маршрута 
    /// </summary>
    [Column("start_time")]
    [Required]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Время окончания маршрута
    /// </summary>
    [Column("end_time")]
    [Required]
    public required DateTime EndTime { get; set; }
}