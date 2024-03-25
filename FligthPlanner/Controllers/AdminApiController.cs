using AutoMapper;
using FligthPlanner.Core.Models;
using FligthPlanner.Core.Services;
using FligthPlanner.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FligthPlanner.Controllers
{
    [ApiController]
    [Authorize]
    [Route("admin-api")]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _service;
        private readonly IAirportService _airServices;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        private readonly object _lock = new object();

        public AdminApiController(IFlightService service,
            IMapper mapper,
            IValidator validator,
            IAirportService airServices)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
            _airServices = airServices;
        }
        [HttpPost("flights")]
        public IActionResult AddFlight(SearchFlightsRequest request)
        {
            var flights = _mapper.Map<Flights>(request);
            var validationResult = _validator.Validate(new ValidationContext<Flights>(flights));

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (_service.GetAll().Any(f => f.From == flights.From && f.To == flights.To))
                return Conflict();

            var addedFlight = _service.CreateFlight(flights);
            var mappedResult = _mapper.Map<SearchFlightsRequest>(addedFlight);

            return Created("", mappedResult);
        }
        [HttpPut("flights")]
        public IActionResult UpdateFlight(AddRequestFlight request)
        {
            lock (_lock)
            {
                var validationResult = _validator.Validate(new ValidationContext<AddRequestFlight>(request));
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var flight = _mapper.Map<Flights>(request);
                if (_service.DuplicateFlight(flight))
                    return Conflict();

                var addedFlight = _service.CreateFlight(flight);
                var mappedResult = _mapper.Map<AddResponseFlight>(addedFlight);

                return Created("", mappedResult);
            }
        }
        [HttpGet("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_lock)
            {
                var flight = _service.FullFlightById(id);
                if (flight == null)
                    return NotFound();

                var mappedResult = _mapper.Map<AddResponseFlight>(flight);

                return Ok(mappedResult);
            }
        }
        [HttpDelete("flights/{id}")]
        public IActionResult RemoveFlight(int id)
        {
            lock (_lock)
            {
                var flight = _service.GetById(id);
                if (flight != null)
                    _service.Delete(flight);
                _airServices.DeleteAirprots();

                return Ok();
            }
        }
    } 
}
