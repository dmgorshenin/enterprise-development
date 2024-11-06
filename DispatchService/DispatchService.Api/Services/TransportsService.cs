using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления транспортными средствами.
/// </summary>
public class TransportsService : IEntityService<Transport, TransportCreateDto, TransportUpdateDto>
{
    private readonly DispatchServiceDbContext _dbContext;

    public TransportsService(DispatchServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получает список всех транспортных средств.
    /// </summary>
    /// <returns>Список объектов <see cref="Transport"/>.</returns>
    public async Task<IEnumerable<Transport>> GetAllAsync() => await _dbContext.Transport.ToListAsync();

    /// <summary>
    /// Получает транспортное средство по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Объект <see cref="Transport"/> или null, если транспортное средство не найдено.</returns>
    public async Task<Transport?> GetByIdAsync(int id) => await _dbContext.Transport.FirstOrDefaultAsync(d => d.Id == id);

    /// <summary>
    /// Добавляет новое транспортное средство.
    /// </summary>
    /// <param name="newTransport">Данные для создания нового транспортного средства.</param>
    /// <returns>Созданное транспортное средство.</returns>
    public async Task<Transport?> AddAsync(TransportCreateDto newTransport)
    {
        var transport = new Transport
        {
            Id = 0,
            LicensePlate = newTransport.LicensePlate,
            ModelName = newTransport.ModelName,
            IsLowFloor = newTransport.IsLowFloor,
            MaxCapacity = newTransport.MaxCapacity,
            Type = newTransport.Type,
            YearOfManufacture = newTransport.YearOfManufacture
        };
        await _dbContext.Transport.AddAsync(transport);
        await _dbContext.SaveChangesAsync();
        return transport;
    }

    /// <summary>
    /// Обновляет данные существующего транспортного средства.
    /// </summary>
    /// <param name="updateTransport">Объект, содержащий обновленные данные транспортного средства.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> UpdateAsync(TransportUpdateDto updateTransport)
    {
        var transport = await GetByIdAsync(updateTransport.TransportId);
        if (transport == null) return false;
        transport.IsLowFloor = updateTransport.IsLowFloor;
        transport.YearOfManufacture = updateTransport.YearOfManufacture;
        transport.MaxCapacity = updateTransport.MaxCapacity;
        transport.LicensePlate = updateTransport.LicensePlate;
        transport.ModelName = updateTransport.ModelName;
        transport.Type = updateTransport.Type;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет транспортное средство по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Значение true, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var transport = await GetByIdAsync(id);
        if (transport == null) return false;
        _dbContext.Transport.Remove(transport);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}