using DispatchService.Api.DTO;
using DispatchService.Api.Services;
using DispatchService.Model;
using Microsoft.AspNetCore.Mvc;

namespace DispatchService.Api.Controllers
{
    /// <summary>
    /// Контроллер транспортных средств
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransportsController(TransportsService transportService, QueryService queryService) : ControllerBase
    {
        /// <summary>
        /// Вывести все ТС
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Transport>> GetTransports()
        {
            return Ok(transportService.GetAll());
        }

        /// <summary>
        /// Вывести ТС по Id
        /// </summary>
        [HttpGet("{id:int}")]
        public ActionResult<Transport> GetTransport(int id)
        {
            var result = transportService.GetById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Добавить ТС
        /// </summary>
        [HttpPost]
        public ActionResult<Transport> CreateTransport(Transport transport)
        {
            var result = transportService.Create(transport);
            return CreatedAtAction(nameof(GetTransport), new { id = result.Id }, result);
        }

        /// <summary>
        /// Обновить ТС
        /// </summary>
        [HttpPut]
        public ActionResult<Transport> UpdateTransport(Transport transport)
        {
            return Ok(transportService.Update(transport));
        }

        /// <summary>
        /// Удалить ТС
        /// </summary>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTransport(int id)
        {
            transportService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Вывести суммарное время поездок ТС каждого типа и модели
        /// </summary>
        [HttpGet("total-trip-times")]
        public ActionResult<List<TotalTripTimesDTO>> GetTotalTripTimesByTransport()
        {
            return Ok(queryService.GetTotalTripTimesByTransport());
        }

        /// <summary>
        /// Вывести информацию о ТС, совершивших максимальное кол. поездок в указанный период
        /// </summary>
        [HttpGet("top-transports")]
        public ActionResult<List<TransportTripCountDTO>> GetTopTransportsByTripCount(DateTime startDate, DateTime endDate)
        {
            return Ok(queryService.GetTopTransportsByTripCount(startDate, endDate));
        }
    }
}
