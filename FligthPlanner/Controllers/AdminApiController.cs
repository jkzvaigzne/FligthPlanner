using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using System.Threading.Tasks;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Authorize]
    [Route("admin-api")]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _lock = new object();
        private readonly FlightStorage _storage;
        public AdminApiController()
        {
            _storage = new FlightStorage();
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id) => NotFound(id);

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flights fligth)
        {
            lock (_lock) 
            { 
            if (!_storage.ValidateFlightsEntry(fligth) || _storage.ValidateFlightDestination(fligth) || !_storage.ValidateFlightsDate(fligth))
            {
                return BadRequest();
            }
            if (_storage.CheckFlightsDuplicateEntry(fligth) != null)
            {
                return Conflict(fligth);
            }
           
            _storage.AddFlight(fligth);

            return Created("", fligth);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult RemoveFlights(int id) 
        {
            lock (_lock) { 
            _storage.RemoveFlight(id);

            return Ok(id);
            }
        }
    }
}