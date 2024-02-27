using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using System.Threading.Tasks;
using FligthPlanner;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Authorize]
    [Route("admin-api")]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _lock = new object();
        private readonly SortedData _data;
        public AdminApiController(FlightPlannerDbContext context)
        {
            _data = new SortedData(context);
            _context = context;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id) 
        {
            var flights = _context.Flights.SingleOrDefault(f => f.Id == id);
            if(flights == null)
            {
                return NotFound();
            }

            return Ok(flights);
        } 

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flights fligth)
        {
            lock (_lock) 
            { 
            if (!_data.ValidateFlightsEntry(fligth) || _data.ValidateFlightDestination(fligth) || !_data.ValidateFlightsDate(fligth))
            {
                return BadRequest();
            }
            if (_data.CheckFlightsDuplicateEntry(fligth) != null)
            {
                return Conflict(fligth);
            }

            _context.Flights.Add(fligth);
            _context.SaveChanges();

            return Created("", fligth);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult RemoveFlights(int id)
        {
            lock (_lock)
            {
                var flightToDelete = _context.Flights.Find(id);

                if (flightToDelete != null)
                {
                    _context.Flights.Remove(flightToDelete);
                    _context.SaveChanges();
                }
            }
            return Ok(id);
        }
    }
}


