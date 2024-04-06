using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FligthPlanner.Core.Services;
using FligthPlanner.Models;

namespace FligthPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAirportService _airService;
        private readonly IFlightService _service;

        public CustomerFlightApiController(IFlightService service, IAirportService airService, IMapper mapper)
            => (_service, _airService, _mapper) = (service, airService, mapper);

        [HttpGet("flights/{id}")]
        public IActionResult FindFlightById(int id)
            => _service.FullFlightById(id) is var flight && flight != null ? Ok(flight) : NotFound();

        [HttpGet("airports")]
        public IActionResult SearchAirports(string search)
            => Ok(_mapper.Map<List<AirportViewModel>>(_airService.SearchAirports(search)));

        [HttpGet("flights/search")]
        public IActionResult SearchFlights(string search)
            => Ok();
    }
}
