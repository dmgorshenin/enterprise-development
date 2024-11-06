using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using DispatchService.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Route = DispatchService.Model.Entities.Route;
namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления маршрутами.
/// </summary>
public class RoutesService : IEntityService<Route, RouteCreateDto, RouteUpdateDto>
{
    private readonly IEntityService<Driver, DriverCreateDto, DriverUpdateDto> _driversService;
    private readonly IEntityService<Transport, TransportCreateDto, TransportUpdateDto> _transportsService;
    private readonly DispatchServiceDbContext _dbContext;

    public RoutesService(
        IEntityService<Driver, DriverCreateDto, DriverUpdateDto> driversService,
        IEntityService<Transport, TransportCreateDto, TransportUpdateDto> transportsService,
        DispatchServiceDbContext dbContext)
    {
        _driversService = driversService;
        _transportsService = transportsService;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получает список всех маршрутов.
    /// </summary>
    /// <returns>Список объектов <see cref="Route"/>.</returns>
    public async Task<IEnumerable<Route>> GetAllAsync() => await _dbContext.Route.Include(r => r.AssignedTransport).Include(r => r.AssignedDriver).ToListAsync();

    /// <summary>
    /// Получает маршрут по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Объект <see cref="Route"/> или null, если маршрут не найден.</returns>
    public async Task<Route?> GetByIdAsync(int id)
    {
        return await _dbContext.Route.Include(r => r.AssignedTransport).Include(r => r.AssignedDriver).FirstOrDefaultAsync(d => d.Id == id);
    }

    /// <summary>
    /// Добавляет новый маршрут.
    /// </summary>
    /// <param name="newRoute">Данные для создания нового маршрута.</param>
    /// <returns>Созданный маршрут или null, если не удалось найти водителя или транспортное средство.</returns>
    public async Task<Route?> AddAsync(RouteCreateDto newRoute)
    {
        var assignedDriver = await _driversService.GetByIdAsync(newRoute.AssignedDriverId);
        var assignedTransport = await _transportsService.GetByIdAsync(newRoute.AssignedTransportId);

        if (assignedDriver == null || assignedTransport == null)
            return null;

        var route = new Route
        {
            Id = 0,
            RouteNumber = newRoute.RouteNumber,
            AssignedDriver = assignedDriver,
            AssignedTransport = assignedTransport,
            StartTime = newRoute.StartTime,
            EndTime = newRoute.EndTime
        };
        await _dbContext.Route.AddAsync(route);
        await _dbContext.SaveChangesAsync();
        return route;
    }

    /// <summary>
    /// Обновляет данные существующего маршрута.
    /// </summary>
    /// <param name="updateRoute">Объект с обновленными данными маршрута.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> UpdateAsync(RouteUpdateDto updateRoute)
    {
        var route = await GetByIdAsync(updateRoute.RouteId);
        if (route == null) return false;

        route.RouteNumber = updateRoute.RouteNumber;
        route.AssignedDriver = await _driversService.GetByIdAsync(updateRoute.AssignedDriverId) ?? route.AssignedDriver;
        route.AssignedTransport = await _transportsService.GetByIdAsync(updateRoute.AssignedTransportId) ?? route.AssignedTransport;
        route.StartTime = updateRoute.StartTime;
        route.EndTime = updateRoute.EndTime;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет маршрут по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Значение true, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var route = await GetByIdAsync(id);
        if (route == null) return false;
        _dbContext.Route.Remove(route);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
