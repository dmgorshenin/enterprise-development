using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления водителями.
/// </summary>
public class DriversService(DispatchServiceDbContext dbContext) : IEntityService<Driver, DriverCreateDto, DriverUpdateDto>
{
    /// <summary>
    /// Получает всех водителей.
    /// </summary>
    /// <returns>Список всех водителей.</returns>
    public async Task<IEnumerable<Driver>> GetAllAsync() => await dbContext.Driver.ToListAsync();

    /// <summary>
    /// Получает водителя по ID.
    /// </summary>
    /// <param name="id">ID водителя.</param>
    /// <returns>Водитель с указанным ID или null, если не найден.</returns>
    public async Task<Driver?> GetByIdAsync(int id) => await dbContext.Driver.FirstOrDefaultAsync(d => d.Id == id);

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="newDriver">DTO с данными нового водителя.</param>
    /// <returns>Добавленный водитель или null, если водитель не был добавлен.</returns>
    public async Task<Driver?> AddAsync(DriverCreateDto newDriver)
    {
        var driver = new Driver
        {
            Id = 0,
            FullName = newDriver.FullName,
            Passport = newDriver.Passport,
            DriverLicense = newDriver.DriverLicense,
            Address = newDriver.Address,
            PhoneNumber = newDriver.PhoneNumber
        };

        await dbContext.Driver.AddAsync(driver);
        await dbContext.SaveChangesAsync();
        return driver;
    }

    /// <summary>
    /// Обновляет существующего водителя.
    /// </summary>
    /// <param name="updateDriver">DTO с обновленными данными водителя.</param>
    /// <returns>True, если водитель был успешно обновлен, иначе false.</returns>
    public async Task<bool> UpdateAsync(DriverUpdateDto updateDriver)
    {
        var driver = await GetByIdAsync(updateDriver.DriverId);
        if (driver == null)
            return false;

        driver.FullName = updateDriver.FullName;
        driver.Passport = updateDriver.Passport;
        driver.DriverLicense = updateDriver.DriverLicense;
        driver.Address = updateDriver.Address;
        driver.PhoneNumber = updateDriver.PhoneNumber;

        await dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет водителя по ID.
    /// </summary>
    /// <param name="id">ID водителя для удаления.</param>
    /// <returns>True, если водитель был успешно удален, иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var driver = await GetByIdAsync(id);
        if (driver == null)
            return false;

        dbContext.Driver.Remove(driver);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
