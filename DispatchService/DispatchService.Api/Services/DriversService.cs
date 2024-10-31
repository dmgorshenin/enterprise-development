using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления водителями.
/// </summary>
public class DriversService(DispatchServiceDbContext storeDispatchServiceDbContext) : IEntityService<Driver, DriverCreateDTO, DriverUpdateDTO>
{
    /// <summary>
    /// Получает всех водителей.
    /// </summary>
    /// <returns>Список всех водителей.</returns>
    public IEnumerable<Driver> GetAll() => storeDispatchServiceDbContext.Driver;

    /// <summary>
    /// Получает водителя по ID.
    /// </summary>
    /// <param name="id">ID водителя.</param>
    /// <returns>Водитель с указанным ID или null, если не найден.</returns>
    public Driver? GetById(int id) => storeDispatchServiceDbContext.Driver.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="newDriver">DTO с данными нового водителя.</param>
    /// <returns>Добавленный водитель или null, если водитель не был добавлен.</returns>
    public Driver? Add(DriverCreateDTO newDriver)
    {
        var driver = new Driver
        {
            Id = 0,
            FullName = newDriver.FullName,
            DriverLicense = newDriver.DriverLicense,
            Passport = newDriver.Passport,
            Address = newDriver.Address,
            PhoneNumber = newDriver.PhoneNumber
        };
        storeDispatchServiceDbContext.Driver.Add(driver);
        storeDispatchServiceDbContext.SaveChanges();
        return driver;
    }

    /// <summary>
    /// Обновляет существующего водителя.
    /// </summary>
    /// <param name="updateDriver">DTO с обновленными данными водителя.</param>
    /// <returns>True, если водитель был успешно обновлен, иначе false.</returns>
    public bool Update(DriverUpdateDTO updateDriver)
    {
        var driver = GetById(updateDriver.DriverId);
        if (driver == null) return false;
        driver.Address = updateDriver.Address;
        driver.PhoneNumber = updateDriver.PhoneNumber;
        driver.DriverLicense = updateDriver.DriverLicense;
        driver.Passport = updateDriver.Passport;
        driver.FullName = updateDriver.FullName;
        storeDispatchServiceDbContext.SaveChanges();
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
        storeDispatchServiceDbContext.Driver.Remove(driver);
        storeDispatchServiceDbContext.SaveChanges();
        return true;
    }
}
