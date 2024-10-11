using DispatchService.Api.Services;
using DispatchService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DispatchService.Api.Controllers
{
    /// <summary>
    /// Контроллер маршрутов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController(RoutesService service) : ControllerBase
    {
        /// <summary>
        /// Вывести все маршруты
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Model.Route>> GetDrivers()
        {
            return Ok(service.GetAll());
        }

        /// <summary>
        /// Вывести маршрут по Id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Model.Route> GetRoute(int id)
        {
            var result = service.GetById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Добавить маршрут
        /// </summary>
        [HttpPost]
        public ActionResult<Model.Route> CreateRoute(Model.Route route)
        {
            var result = service.Create(route);
            return CreatedAtAction(nameof(GetRoute), new { id = result.Id }, result);
        }

        /// <summary>
        /// Обновить маршрут
        /// </summary>
        [HttpPut]
        public ActionResult<Model.Route> UpdateRoute(Model.Route route)
        {
            return Ok(service.Update(route));
        }

        /// <summary>
        /// Удалить маршрут
        /// </summary>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRoute(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}
