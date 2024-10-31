using DispatchService.Api.DTO;
using DispatchService.Model.Context;
using Microsoft.EntityFrameworkCore;
using Route = DispatchService.Model.Entities.Route;
namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления маршрутами.
/// </summary>
public class RoutesService(DriversService driversService, TransportsService transportsService, DispatchServiceDbContext storeDispatchServiceDbContext) : IEntityService<Route, RouteCreateDTO, RouteUpdateDTO>
{
    /// <summary>
    /// Получает список всех маршрутов.
    /// </summary>
    /// <returns>Список объектов <see cref="Route"/>.</returns>
    public IEnumerable<Route> GetAll() => storeDispatchServiceDbContext.Route.Include(r => r.AssignedTransport).Include(r => r.AssignedDriver);

    /// <summary>
    /// Получает маршрут по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Объект <see cref="Route"/> или null, если маршрут не найден.</returns>
    public Route? GetById(int id)
    {
        return storeDispatchServiceDbContext.Route.Include(r => r.AssignedTransport).Include(r => r.AssignedDriver).FirstOrDefault(d => d.Id == id);
    }

    /// <summary>
    /// Добавляет новый маршрут.
    /// </summary>
    /// <param name="newRoute">Данные для создания нового маршрута.</param>
    /// <returns>Созданный маршрут или null, если не удалось найти водителя или транспортное средство.</returns>
    public Route? Add(RouteCreateDTO newRoute)
    {
        var assignedDriver = driversService.GetById(newRoute.AssignedDriverId);
        var assignedTransport = transportsService.GetById(newRoute.AssignedTransportId);

        if (assignedDriver == null) return null;
        if (assignedTransport == null) return null;

        var route = new Route
        {
            Id = 0,
            RouteNumber = newRoute.RouteNumber,
            AssignedDriver = assignedDriver,
            AssignedTransport = assignedTransport,
            StartTime = newRoute.StartTime,
            EndTime = newRoute.EndTime
        };
        storeDispatchServiceDbContext.Route.Add(route);
        storeDispatchServiceDbContext.SaveChanges();
        return route;
    }

    /// <summary>
    /// Обновляет данные существующего маршрута.
    /// </summary>
    /// <param name="updateRoute">Объект с обновленными данными маршрута.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public bool Update(RouteUpdateDTO updateRoute)
    {
        var route = GetById(updateRoute.RouteId);
        var driver = driversService.GetById(updateRoute.AssignedDriverId);
        var transport = transportsService.GetById(updateRoute.AssignedTransportId);
        if (route == null) return false;
        route.RouteNumber = updateRoute.RouteNumber;
        if (driver != null)
        {
            route.AssignedDriver = driver;
        }
        if (transport != null)
        {
            route.AssignedTransport = transport;
        }
        route.StartTime = updateRoute.StartTime;
        route.EndTime = updateRoute.EndTime;
        storeDispatchServiceDbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// Удаляет маршрут по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Значение true, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var route = GetById(id);
        if (route == null) return false;
        storeDispatchServiceDbContext.Route.Remove(route);
        storeDispatchServiceDbContext.SaveChanges();
        return true;
    }
}
