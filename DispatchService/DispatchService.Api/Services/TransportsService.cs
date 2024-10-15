using DispatchService.Api.DTO;
using DispatchService.Model;

namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления транспортными средствами.
/// </summary>
public class TransportsService : IEntityService<Transport, TransportCreateDTO, TransportDTO>
{
    private List<Transport> _transports = [];
    private int _id = 1;

    /// <summary>
    /// Получает список всех транспортных средств.
    /// </summary>
    /// <returns>Список объектов <see cref="Transport"/>.</returns>
    public List<Transport> GetAll() => _transports;

    /// <summary>
    /// Получает транспортное средство по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Объект <see cref="Transport"/> или null, если транспортное средство не найдено.</returns>
    public Transport? GetById(int id) => _transports.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Добавляет новое транспортное средство.
    /// </summary>
    /// <param name="newTransport">Данные для создания нового транспортного средства.</param>
    /// <returns>Созданное транспортное средство.</returns>
    public Transport? Add(TransportCreateDTO newTransport)
    {
        var transport = new Transport
        {
            Id = _id++,
            LicensePlate = newTransport.LicensePlate,
            ModelName = newTransport.ModelName,
            IsLowFloor = newTransport.IsLowFloor,
            MaxCapacity = newTransport.MaxCapacity,
            Type = (Model.VehicleType)newTransport.Type,
            YearOfManufacture = newTransport.YearOfManufacture
        };
        _transports.Add(transport);
        return _transports.FirstOrDefault(d => d.Id == transport.Id);
    }

    /// <summary>
    /// Обновляет данные существующего транспортного средства.
    /// </summary>
    /// <param name="updateTransport">Объект, содержащий обновленные данные транспортного средства.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public bool Update(TransportDTO updateTransport)
    {
        var transport = GetById(updateTransport.Id);
        if (transport == null) return false;
        transport.IsLowFloor = updateTransport.IsLowFloor;
        transport.YearOfManufacture = updateTransport.YearOfManufacture;
        transport.MaxCapacity = updateTransport.MaxCapacity;
        transport.LicensePlate = updateTransport.LicensePlate;
        transport.ModelName = updateTransport.ModelName;
        transport.Type = (Model.VehicleType)updateTransport.Type;
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
        return _transports.Remove(transport);
    }
}