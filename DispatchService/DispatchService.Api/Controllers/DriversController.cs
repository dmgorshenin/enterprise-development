using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using DispatchService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DispatchService.Api.Controllers
{
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
        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDrivers()
        {
            return Ok(driverService.GetAll());
        }

        /// <summary>
        /// Вывести водителя по Id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Driver> GetDriver(int id)
        {
            var result = driverService.GetById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Добавить водителя
        /// </summary>
        [HttpPost]
        public ActionResult<Driver> CreateDriver(Driver driver)
        {
            var result = driverService.Create(driver);
            return CreatedAtAction(nameof(GetDriver), new { id = result.Id }, result);
        }

        /// <summary>
        /// Обновить водителя
        /// </summary>
        [HttpPut]
        public ActionResult<Driver> UpdateDriver(Driver driver)
        {
            return Ok(driverService.Update(driver));
        }

        /// <summary>
        /// Удалить водителя
        /// </summary>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDriver(int id)
        {
            driverService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Вывести всех водителей, совершивших поездки в заданный период
        /// </summary>
        [HttpGet("drivers-in-period")]
        public ActionResult<List<Driver>> GetDriversInPeriod(DateTime startDate, DateTime endDate)
        {
            return Ok(queryService.GetDriversInPeriod(startDate, endDate));
        }

        /// <summary>
        /// Вывести топ водителей по количеству поездок
        /// </summary>
        [HttpGet("top-drivers")]
        public ActionResult<List<DriverTripCountDTO>> GetTopDriversByTripCount()
        {
            return Ok(queryService.GetTopDriversByTripCount());
        }

        /// <summary>
        /// Вывести информацию по каждому водителю
        /// </summary>
        [HttpGet("driver-trip-stats")]
        public ActionResult<List<DriverTripStatsDTO>> GetDriverTripStats()
        {
            return Ok(queryService.GetDriverTripStats());
        }
    }
}
