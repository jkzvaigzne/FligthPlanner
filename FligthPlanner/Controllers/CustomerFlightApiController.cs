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
        private readonly FlightStorage _storage = new();

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var flight = _storage.SearchAirport(search);

            if (flight != null)
            {
                var res = new[] { flight.From };
                return Ok(res);
            }
            else
            {
                return NotFound("airport not found.");
            }
        }
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightById(int id)
        {
            var result = _storage.SearchFlightById(id);

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
            var res = _storage.SearchFlight(request);
            
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