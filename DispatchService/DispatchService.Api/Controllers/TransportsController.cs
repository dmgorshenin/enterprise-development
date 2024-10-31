using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using DispatchService.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DispatchService.Api.Controllers;

/// <summary>
/// Контроллер транспортных средств
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TransportsController(TransportsService transportService, QueryService queryService) : ControllerBase
{
    /// <summary>
    /// Вывести все транспортные средства
    /// </summary>
    /// <returns>Список всех транспортных средств.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает список транспортных средств.</response>
    /// <response code="404">Транспортные средства не найдены.</response>
    [HttpGet]
    public ActionResult<IEnumerable<Transport>> Get()
    {
        var result = transportService.GetAll();
        if (result == null) return NotFound($"Транспортные средства не найдены.");
        return Ok(result);
    }

    /// <summary>
    /// Вывести транспортное средство по Id
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Информация о транспортном средстве.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает транспортное средство с указанным идентификатором.</response>
    /// <response code="404">Транспортное средство с указанным идентификатором не найдено.</response>
    [HttpGet("{id:int}")]
    public ActionResult<Transport> Get(int id)
    {
        var result = transportService.GetById(id);
        if (result == null) return NotFound($"Транспортное средство с ID {id} не найдено.");
        return Ok(result);
    }

    /// <summary>
    /// Добавить транспортное средство
    /// </summary>
    /// <param name="transport">Данные для создания нового транспортного средства.</param>
    /// <returns>Созданное транспортное средство.</returns>
    /// <response code="200">Транспортное средство успешно добавлено.</response>
    /// <response code="400">Некорректные данные для создания транспортного средства.</response>
    [HttpPost]
    public ActionResult<Transport> Post(TransportCreateDTO transport)
    {
        if (transport == null) return BadRequest("Данные транспортного средства не могут быть пустыми.");
        var result = transportService.Add(transport);
        if (result == null) return BadRequest("Некорректные данные транспортного средства.");
        return Ok(result);
    }

    /// <summary>
    /// Обновить транспортное средство
    /// </summary>
    /// <param name="transport">Данные для обновления транспортного средства.</param>
    /// <returns>Результат обновления транспортного средства.</returns>
    /// <response code="200">Транспортное средство успешно обновлено.</response>
    /// <response code="400">Некорректные данные для обновления транспортного средства.</response>
    /// <response code="404">Транспортное средство с указанным идентификатором не найдено.</response>
    [HttpPut]
    public ActionResult<Transport> Put(TransportUpdateDTO transport)
    {
        if (transport == null || transport.TransportId == 0) return BadRequest("Некорректные данные транспортного средства.");
        var success = transportService.Update(transport);
        if (!success) return NotFound($"Транспортное средство с ID {transport.TransportId} не найдено.");
        return Ok("Транспортное средство успешно обновлено.");
    }

    /// <summary>
    /// Удалить ТС
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства.</param>
    /// <returns>Результат удаления транспортного средства.</returns>
    /// <response code="200">Транспортное средство успешно удалено.</response>
    /// <response code="404">Транспортное средство с указанным идентификатором не найдено.</response>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = transportService.Delete(id);
        if (!success) return NotFound($"Транспортное средство с ID {id} не найдено.");
        return Ok("Транспортное средство успешно удалено.");
    }

    /// <summary>
    /// Вывести суммарное время поездок ТС каждого типа и модели
    /// </summary>
    /// <returns>Список моделей и типов ТС с их суммарным временем поездок.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает суммарное время поездок ТС.</response>
    [HttpGet]
    [Route("total-trip-times")]
    public ActionResult<List<TotalTripTimesDTO>> GetTotalTripTimesByTransport()
    {
        return Ok(queryService.GetTotalTripTimesByTransport());
    }

    /// <summary>
    /// Вывести информацию о ТС, совершивших максимальное кол. поездок в указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода.</param>
    /// <param name="endDate">Дата окончания периода.</param>
    /// <returns>Список транспортных средств с максимальным количеством поездок за период.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает транспортные средства с максимальным количеством поездок.</response>
    /// <response code="400">Некорректные даты периода.</response>
    [HttpGet]
    [Route("top-transports")]
    public ActionResult<List<TransportTripCountDTO>> GetTopTransportsByTripCount(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate) return BadRequest("Дата начала не может быть позже даты окончания.");
        return Ok(queryService.GetTopTransportsByTripCount(startDate, endDate));
    }
}
