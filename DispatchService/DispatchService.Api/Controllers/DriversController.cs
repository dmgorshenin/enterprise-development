using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using DispatchService.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DispatchService.Api.Controllers;

/// <summary>
/// Контроллер водителей
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DriversController(DriversService driverService, QueryService queryService) : ControllerBase
{
    /// <summary>
    /// Вывести всех водителей
    /// </summary>
    /// <returns>Список всех водителей.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает список водителей.</response>
    /// <response code="404">Водители не найдены.</response>
    [HttpGet]
    public ActionResult<IEnumerable<Driver>> Get()
    {
        var result = driverService.GetAll();
        if (result == null) return NotFound("Водител не найдены.");
        return Ok(result);
    }

    /// <summary>
    /// Вывести водителя по Id
    /// </summary>
    /// <param name="id">Идентификатор водителя.</param>
    /// <returns>Информация о водителе.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает водителя с указанным идентификатором.</response>
    /// <response code="404">Водитель с указанным идентификатором не найден.</response>
    [HttpGet("{id:int}")]
    public ActionResult<Driver> Get(int id)
    {
        var result = driverService.GetById(id);
        if (result == null) return NotFound($"Водитель с ID {id} не найден.");
        return Ok(result);
    }

    /// <summary>
    /// Добавить водителя
    /// </summary>
    /// <param name="driver">Данные для создания нового водителя.</param>
    /// <returns>Созданный водитель.</returns>
    /// <response code="200">Водитель успешно добавлен.</response>
    /// <response code="400">Некорректные данные для создания водителя.</response>
    [HttpPost]
    public ActionResult<Driver> Post(DriverCreateDTO driver)
    {
        if (driver == null) return BadRequest("Данные водителя не могут быть пустыми.");
        var result = driverService.Add(driver);
        if (result == null) return BadRequest("Некорректные данные водителя.");
        return Ok(result);
    }

    /// <summary>
    /// Обновить водителя
    /// </summary>
    /// <param name="driver">Данные для обновления водителя.</param>
    /// <returns>Результат обновления водителя.</returns>
    /// <response code="200">Водитель успешно обновлен.</response>
    /// <response code="400">Некорректные данные для обновления водителя.</response>
    /// <response code="404">Водитель с указанным идентификатором не найден.</response>
    [HttpPut]
    public ActionResult<Driver> Put(DriverUpdateDTO driver)
    {
        if (driver == null || driver.DriverId == 0) return BadRequest("Некорректные данные водителя.");
        var success = driverService.Update(driver);
        if (!success) return NotFound($"Водитель с ID {driver.DriverId} не найден.");
        return Ok("Водитель успешно обновлен.");
    }

    /// <summary>
    /// Удалить водителя
    /// </summary>
    /// <param name="id">Идентификатор водителя.</param>
    /// <returns>Результат удаления водителя.</returns>
    /// <response code="200">Водитель успешно удален.</response>
    /// <response code="404">Водитель с указанным идентификатором не найден.</response>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteDriver(int id)
    {
        var success = driverService.Delete(id);
        if (!success) return NotFound($"Водитель с ID {id} не найден.");
        return Ok("Водитель успешно удален.");
    }

    /// <summary>
    /// Вывести всех водителей, совершивших поездки в заданный период
    /// </summary>
    /// <param name="startDate">Дата начала периода.</param>
    /// <param name="endDate">Дата окончания периода.</param>
    /// <returns>Список водителей, совершивших поездки в указанный период.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает список водителей.</response>
    /// <response code="400">Некорректный период дат.</response>
    [HttpGet]
    [Route("drivers-in-period")]
    public ActionResult<List<Driver>> GetDriversInPeriod(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate) return BadRequest("Дата начала не может быть позже даты окончания.");
        return Ok(queryService.GetDriversInPeriod(startDate, endDate));
    }

    /// <summary>
    /// Вывести топ водителей по количеству поездок
    /// </summary>
    /// <returns>Список водителей с наибольшим количеством поездок.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает топ водителей.</response>
    [HttpGet]
    [Route("top-drivers")]
    public ActionResult<List<DriverTripCountDTO>> GetTopDriversByTripCount()
    {
        return Ok(queryService.GetTopDriversByTripCount());
    }

    /// <summary>
    /// Вывести информацию по каждому водителю
    /// </summary>
    /// <returns>Список статистики по водителям и их поездкам.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает статистику поездок.</response>
    [HttpGet]
    [Route("driver-trip-stats")]
    public ActionResult<List<DriverTripStatsDTO>> GetDriverTripStats()
    {
        return Ok(queryService.GetDriverTripStats());
    }
}
