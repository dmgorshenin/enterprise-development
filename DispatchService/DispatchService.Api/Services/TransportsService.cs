using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления транспортными средствами.
/// </summary>
public class TransportsService(DispatchServiceDbContext storeDispatchServiceDbContext) : IEntityService<Transport, TransportCreateDTO, TransportUpdateDTO>
{
    /// <summary>
    /// Получает список всех транспортных средств.
    /// </summary>
    /// <returns>Список объектов <see cref="Transport"/>.</returns>
    public IEnumerable<Transport> GetAll() => storeDispatchServiceDbContext.Transport;

    /// <summary>
    /// Получает транспортное средство по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Объект <see cref="Transport"/> или null, если транспортное средство не найдено.</returns>
    public Transport? GetById(int id) => storeDispatchServiceDbContext.Transport.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Добавляет новое транспортное средство.
    /// </summary>
    /// <param name="newTransport">Данные для создания нового транспортного средства.</param>
    /// <returns>Созданное транспортное средство.</returns>
    public Transport? Add(TransportCreateDTO newTransport)
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
        storeDispatchServiceDbContext.Transport.Add(transport);
        storeDispatchServiceDbContext.SaveChanges();
        return transport;
    }

    /// <summary>
    /// Обновляет данные существующего транспортного средства.
    /// </summary>
    /// <param name="updateTransport">Объект, содержащий обновленные данные транспортного средства.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public bool Update(TransportUpdateDTO updateTransport)
    {
        var transport = GetById(updateTransport.TransportId);
        if (transport == null) return false;
        transport.IsLowFloor = updateTransport.IsLowFloor;
        transport.YearOfManufacture = updateTransport.YearOfManufacture;
        transport.MaxCapacity = updateTransport.MaxCapacity;
        transport.LicensePlate = updateTransport.LicensePlate;
        transport.ModelName = updateTransport.ModelName;
        transport.Type = updateTransport.Type;
        storeDispatchServiceDbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// Удаляет транспортное средство по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Значение true, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var transport = GetById(id);
        if (transport == null) return false;
        storeDispatchServiceDbContext.Transport.Remove(transport);
        storeDispatchServiceDbContext.SaveChanges();
        return true;
    }
}