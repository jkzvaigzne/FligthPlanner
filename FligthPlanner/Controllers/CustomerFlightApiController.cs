using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Storage;
using FlightPlanner.Models;
using FligthPlanner.Models;

namespace FligthPlanner.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerFlightApiController : ControllerBase
    {
        private readonly DBData _data;

        public CustomerFlightApiController(FlightPlannerDbContext context)
        {
            _data = new DBData(context);
        }
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport([FromQuery] string search)
        {
            var flight = _data.SearchAirport(search);
            return Ok(flight);
        }


        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightById(int id)
        {
            var result = _data.SearchFlightById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPost]
        [Route("flights/search")]
        public IActionResult GetFlights(SearchFlightsRequest request)
        {
            var res = _data.SearchFlight(request);
            
            if (request.From == request.To)
            {
                return BadRequest();
            }

            var pageRes = new PageResult<Flights>
            {
                Page = 0,
                TotalItems = res.Count(),
                Items = res
            };

            return Ok(pageRes);
        }
    }
}