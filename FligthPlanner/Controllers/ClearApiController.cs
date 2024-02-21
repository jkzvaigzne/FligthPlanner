﻿using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class ClearApiController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public ClearApiController()
        {
            _storage = new FlightStorage();
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult ClearFlights()
        {
            _storage.ClearFlights();

            return Ok();
        }
    }
}