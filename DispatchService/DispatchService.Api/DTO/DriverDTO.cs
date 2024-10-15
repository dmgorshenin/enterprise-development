namespace DispatchService.Api.DTO;

/// <summary>
/// DTO водителя
/// </summary>
public class DriverDTO
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// Номер лицензии водителя
    /// </summary>
    public required string DriverLicense { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}
