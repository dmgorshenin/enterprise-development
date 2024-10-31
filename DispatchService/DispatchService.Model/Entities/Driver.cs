using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchService.Model.Entities;

/// <summary>
/// Водитель
/// </summary>
[Table("driver")]
public class Driver
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    [Column("fullname")]
    [MaxLength(100)]
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    [Column("passport")]
    [MaxLength(10)]
    [Required]
    public required string Passport { get; set; }

    /// <summary>
    /// Номер лицензии водителя
    /// </summary>
    [Column("driver_license")]
    [MaxLength(10)]
    [Required]
    public required string DriverLicense { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [Column("address")]
    [MaxLength(100)]
    [Required]
    public required string Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Column("phone_number")]
    [MaxLength(100)]
    [Required]
    public required string PhoneNumber { get; set; }
}