using DispatchService.Api.DTO;
using Route = DispatchService.Model.Route;
namespace DispatchService.Api.Services;

/// <summary>
/// Сервис для управления маршрутами.
/// </summary>
public class RoutesService(DriversService driversService, TransportsService transportsService) : IEntityService<Route, RouteCreateDTO, RouteDTO>
{
    private readonly List<Route> _routes = [];
    private int _id = 1;

    /// <summary>
    /// Получает список всех маршрутов.
    /// </summary>
    /// <returns>Список объектов <see cref="Route"/>.</returns>
    public List<Route> GetAll() => _routes;

    /// <summary>
    /// Получает маршрут по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Объект <see cref="Route"/> или null, если маршрут не найден.</returns>
    public Route? GetById(int id) => _routes.FirstOrDefault(d => d.Id == id);

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
            Id = _id++,
            RouteNumber = newRoute.RouteNumber,
            AssignedDriver = assignedDriver,
            AssignedTransport = assignedTransport,
            StartTime = newRoute.StartTime,
            EndTime = newRoute.EndTime
        };
        _routes.Add(route);
        return _routes.FirstOrDefault(d => d.Id == route.Id);
    }

    /// <summary>
    /// Обновляет данные существующего маршрута.
    /// </summary>
    /// <param name="updateRoute">Объект с обновленными данными маршрута.</param>
    /// <returns>Значение true, если обновление прошло успешно; иначе false.</returns>
    public bool Update(RouteDTO updateRoute)
    {
        var route = GetById(updateRoute.Id);
        if (route == null) return false;
        route.RouteNumber = updateRoute.RouteNumber;
        route.AssignedDriver = updateRoute.AssignedDriver;
        route.AssignedTransport = updateRoute.AssignedTransport;
        route.StartTime = updateRoute.StartTime;
        route.EndTime = updateRoute.EndTime;
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
        return _routes.Remove(route);
    }
}
