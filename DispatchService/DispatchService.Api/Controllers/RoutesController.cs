using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Route = DispatchService.Model.Entities.Route;

namespace DispatchService.Api.Controllers;

/// <summary>
/// Контроллер маршрутов
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RoutesController(IEntityService<Route, RouteCreateDto, RouteUpdateDto> routeService) : ControllerBase
{
    /// <summary>
    /// Вывести все маршруты
    /// </summary>
    /// <returns>Список всех маршрутов.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает список маршрутов.</response>
    /// <response code="404">Маршруты не найдены.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Route>>> Get()
    {
        var result = await routeService.GetAllAsync();
        if (result == null) return NotFound("Маршруты не найдены.");
        return Ok(result);
    }

    /// <summary>
    /// Вывести маршрут по Id
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Маршрут с заданным идентификатором или ошибка, если маршрут не найден.</returns>
    /// <response code="200">Запрос выполнен успешно, возвращает маршрут с указанным идентификатором.</response>
    /// <response code="404">Маршрут с указанным идентификатором не найден.</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Route>> Get(int id)
    {
        var result = await routeService.GetByIdAsync(id);
        if (result == null) return NotFound($"Маршрут с ID {id} не найден.");
        return Ok(result);
    }

    /// <summary>
    /// Добавить маршрут
    /// </summary>
    /// <param name="route">Данные для создания нового маршрута.</param>
    /// <returns>Созданный маршрут и код 201 (Created) при успешном создании.</returns>
    /// <response code="200">Маршрут успешно создан.</response>
    /// <response code="400">Некорректные данные для создания маршрута.</response>
    [HttpPost]
    public async Task<ActionResult<Route>> Post(RouteCreateDto route)
    {
        if (route == null) return BadRequest("Данные маршрута не могут быть пустыми.");
        var result = await routeService.AddAsync(route);
        if (result == null) return BadRequest("Некорректные данные для маршрута.");
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновить маршрут
    /// </summary>
    /// <param name="route">Данные для обновления маршрута.</param>
    /// <returns>Обновленный маршрут или ошибка.</returns>
    /// <response code="200">Маршрут успешно обновлен.</response>
    /// <response code="400">Некорректные данные для обновления маршрута.</response>
    /// <response code="404">Маршрут с указанным идентификатором не найден.</response>
    [HttpPut]
    public async Task<ActionResult> Put(RouteUpdateDto route)
    {
        if (route == null || route.RouteId == 0) return BadRequest("Некорректные данные для маршрута.");
        var success = await routeService.UpdateAsync(route);
        if (!success) return NotFound($"Маршрут с ID {route.RouteId} не найден.");
        return Ok("Маршрут успешно обновлен.");
    }

    /// <summary>
    /// Удалить маршрут
    /// </summary>
    /// <param name="id">Идентификатор маршрута.</param>
    /// <returns>Ответ с кодом 200 при успешном удалении или код ошибки.</returns>
    /// <response code="200">Маршрут успешно удален.</response>
    /// <response code="404">Маршрут с указанным идентификатором не найден.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await routeService.DeleteAsync(id);
        if (!success) return NotFound($"Маршрут с ID {id} не найден.");
        return Ok("Маршрут успешно удален.");
    }
}
