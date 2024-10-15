using DispatchService.Api.DTO;
using DispatchService.Model;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления водителями.
/// </summary>
public class DriversService : IEntityService<Driver, DriverCreateDTO, DriverDTO>
{
    private readonly List<Driver> _drivers = [];
    private int _id = 1;

    /// <summary>
    /// Получает всех водителей.
    /// </summary>
    /// <returns>Список всех водителей.</returns>
    public List<Driver> GetAll() => _drivers;

    /// <summary>
    /// Получает водителя по ID.
    /// </summary>
    /// <param name="id">ID водителя.</param>
    /// <returns>Водитель с указанным ID или null, если не найден.</returns>
    public Driver? GetById(int id) => _drivers.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="newDriver">DTO с данными нового водителя.</param>
    /// <returns>Добавленный водитель или null, если водитель не был добавлен.</returns>
    public Driver? Add(DriverCreateDTO newDriver)
    {
        var driver = new Driver
        {
            Id = _id++,
            FullName = newDriver.FullName,
            DriverLicense = newDriver.DriverLicense,
            Passport = newDriver.Passport,
            Address = newDriver.Address,
            PhoneNumber = newDriver.PhoneNumber
        };
        _drivers.Add(driver);
        return _drivers.FirstOrDefault(d => d.Id == driver.Id);
    }

    /// <summary>
    /// Обновляет существующего водителя.
    /// </summary>
    /// <param name="updateDriver">DTO с обновленными данными водителя.</param>
    /// <returns>True, если водитель был успешно обновлен, иначе false.</returns>
    public bool Update(DriverDTO updateDriver)
    {
        var driver = GetById(updateDriver.Id);
        if (driver == null) return false;
        driver.Address = updateDriver.Address;
        driver.PhoneNumber = updateDriver.PhoneNumber;
        driver.DriverLicense = updateDriver.DriverLicense;
        driver.Passport = updateDriver.Passport;
        driver.FullName = updateDriver.FullName;
        return true;
    }

    /// <summary>
    /// Удаляет водителя по ID.
    /// </summary>
    /// <param name="id">ID водителя для удаления.</param>
    /// <returns>True, если водитель был успешно удален, иначе false.</returns>
    public bool Delete(int id)
    {
        var driver = GetById(id);
        if (driver == null) return false;
        return _drivers.Remove(driver);
    }
}
